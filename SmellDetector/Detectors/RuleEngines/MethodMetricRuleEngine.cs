﻿using CodeModel.CaDETModel.CodeItems;
using SmellDetector.SmellModel;
using SmellDetector.SmellModel.Reports;
using System.Collections.Generic;
using System.Linq;

namespace SmellDetector.Detectors.RuleEngines
{
    public class MethodMetricRuleEngine : IDetector
    {
        private readonly List<Rule> _rules;
        private readonly List<Rule> _dynamicRules;

        public MethodMetricRuleEngine()
        {
            _rules = new List<Rule>();
            _dynamicRules = new List<Rule>();
            Rule rule1 = new Rule("10.1109/SCAM.2013.6648192",
                                  new MetricCriteria(CaDETMetric.MLOC, OperationEnum.GREATER_THAN, 50),
                                  SmellType.LONG_METHOD);
            Rule rule2 = new Rule("10.1016/j.jss.2006.10.018",
                                  new OrCriteria(
                                      new MetricCriteria(CaDETMetric.MLOC, OperationEnum.GREATER_THAN, 50),
                                      new MetricCriteria(CaDETMetric.CYCLO, OperationEnum.GREATER_THAN, 10)),
                                  SmellType.LONG_METHOD);
            Rule rule3 = new Rule("https://doi.org/10.1145/3132498.3134268",
                                  new AndCriteria(
                                      new AndCriteria(
                                          new MetricCriteria(CaDETMetric.MLOC, OperationEnum.GREATER_THAN, 30),
                                          new MetricCriteria(CaDETMetric.CYCLO, OperationEnum.GREATER_THAN, 4)),
                                      new MetricCriteria(CaDETMetric.MMNB, OperationEnum.GREATER_THAN, 3)),
                                  SmellType.LONG_METHOD);
            _rules.Add(rule1);
            _rules.Add(rule2);
            _rules.Add(rule3);
        }

        private void DefineSpecialRuleFromLiArticle(List<CaDETMember> methods)
        {
            int indexOfSignificantMetricValue = CalculateIndexBasedOnPercentage(methods, 20);
            double mlocThreshold = FindTopXMetricValuesInProject(methods, CaDETMetric.MLOC, indexOfSignificantMetricValue);

            Rule rule1 = new Rule("10.1016/j.jss.2006.10.018",
                                                new AndCriteria(new AndCriteria(new MetricCriteria(CaDETMetric.MLOC, OperationEnum.GREATER_OR_EQUALS, mlocThreshold),
                                                                                    new MetricCriteria(CaDETMetric.MLOC, OperationEnum.GREATER_OR_EQUALS, 70)),
                                                                new AndCriteria(new OrCriteria(new MetricCriteria(CaDETMetric.NOP, OperationEnum.GREATER_THAN, 4),
                                                                                                new MetricCriteria(CaDETMetric.NOLV, OperationEnum.GREATER_THAN, 4)),
                                                                                new MetricCriteria(CaDETMetric.CYCLO, OperationEnum.GREATER_THAN, 4))),
                                SmellType.LONG_METHOD);
            _dynamicRules.Add(rule1);
        }
        
        private double FindTopXMetricValuesInProject(List<CaDETMember> methods, CaDETMetric metric, int indexOfMetricValue)
        {
            List<double> metricValues = methods.Select(c => c.Metrics[metric]).ToList();
            metricValues.Sort();
            return metricValues[indexOfMetricValue];
        }

        private int CalculateIndexBasedOnPercentage(List<CaDETMember> methods, int percentage)
        {
            return methods.Count * percentage / 100;
        }

        private void DefineTopXMetricRules(List<CaDETMember> methods)
        {
            double mlocThreshold = CalculateAverageMLOCForProject(methods);

            Rule rule1 = new Rule("https://doi.org/10.1016/j.jss.2015.05.024",
                                        new MetricCriteria(CaDETMetric.MLOC, OperationEnum.GREATER_THAN, mlocThreshold),
                                    SmellType.LONG_METHOD);
            _dynamicRules.Add(rule1);
        }

        private double CalculateAverageMLOCForProject(List<CaDETMember> methods)
        {
            List<double> metricValues = methods.Select(c => c.Metrics[CaDETMetric.MLOC]).ToList();
            double average = metricValues.Count > 0 ? metricValues.Average() : 0.0;
            return average;
        }

        public PartialSmellDetectionReport FindIssues(List<CaDETClass> classes)
        {
            List<CaDETMember> methods = classes.SelectMany(c => c.Members).ToList();
            DefineTopXMetricRules(methods);

            var partialReport = new PartialSmellDetectionReport();

            foreach (var method in methods)
            {
                DefineSpecialRuleFromLiArticle(method.Parent.Members);
                var issues = ApplyRules(method);
                foreach (var issue in issues.Where(issue => issue != null))
                {
                    partialReport.AddIssue(issue.CodeSnippetId, issue);
                }
            }
            return partialReport;
        }

        private List<Issue> ApplyRules(CaDETMember m)
        {
            List<Issue> issues = _rules.Select(r => r.Validate(m.Name, m.Metrics)).ToList();
            issues.AddRange(_dynamicRules.Select(r => r.Validate(m.Name, m.Metrics)).ToList());
            return issues;
        }
    }
}
