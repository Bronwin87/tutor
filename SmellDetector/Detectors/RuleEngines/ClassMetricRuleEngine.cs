﻿using CodeModel.CaDETModel.CodeItems;
using SmellDetector.SmellModel;
using SmellDetector.SmellModel.Reports;
using System.Collections.Generic;
using System.Linq;

namespace SmellDetector.Detectors.RuleEngines
{
    internal class ClassMetricRuleEngine : IDetector
    {
        private readonly List<Rule> _rules;
        private readonly List<Rule> _dynamicRules;
        public ClassMetricRuleEngine()
        {
            Rule rule1 = new Rule("10.1109/WCRE.2005.15",
                                  new AndCriteria(
                                            new AndCriteria(
                                                new MetricCriteria(CaDETMetric.ATFD, OperationEnum.GREATER_THAN, 2),
                                                new MetricCriteria(CaDETMetric.WMC, OperationEnum.GREATER_OR_EQUALS, 47)),
                                            new MetricCriteria(CaDETMetric.TCC, OperationEnum.LESS_THAN, 0.33)),
                                  SmellType.GOD_CLASS);
            Rule rule2 = new Rule("https://doi.org/10.1145/2162049.2162069",
                                  new AndCriteria(
                                            new AndCriteria(
                                                new MetricCriteria(CaDETMetric.ATFD, OperationEnum.GREATER_THAN, 5),
                                                new MetricCriteria(CaDETMetric.WMC, OperationEnum.GREATER_OR_EQUALS, 47)),
                                            new MetricCriteria(CaDETMetric.TCC, OperationEnum.LESS_THAN, 0.3)),
                                  SmellType.GOD_CLASS);
            Rule rule3 = new Rule("10.1109/MSR.2007.21",
                                  new OrCriteria(
                                            new MetricCriteria(CaDETMetric.NMD, OperationEnum.GREATER_THAN, 15),
                                            new MetricCriteria(CaDETMetric.NAD, OperationEnum.GREATER_THAN, 15)),
                                  SmellType.GOD_CLASS);
            Rule rule4 = new Rule("10.1109/SCAM.2013.6648192",
                                  new OrCriteria(
                                      new MetricCriteria(CaDETMetric.CLOC, OperationEnum.GREATER_THAN, 750),
                                      new MetricCriteria(CaDETMetric.NMD_NAD, OperationEnum.GREATER_THAN, 20)
                                      ),
                                  SmellType.GOD_CLASS);
            Rule rule5 = new Rule("10.1109/TSE.2009.50",
                                   new MetricCriteria(CaDETMetric.NMD_NAD, OperationEnum.GREATER_THAN, 20),
                                   SmellType.GOD_CLASS);
            Rule rule6 = new Rule("10.1109/ESEM.2009.5314231",
                                  new AndCriteria(
                                            new AndCriteria(
                                                new MetricCriteria(CaDETMetric.ATFD, OperationEnum.GREATER_THAN, 5),
                                                new MetricCriteria(CaDETMetric.WMC, OperationEnum.GREATER_OR_EQUALS, 47)),
                                            new MetricCriteria(CaDETMetric.TCC, OperationEnum.LESS_THAN, 1/3)),
                                  SmellType.GOD_CLASS);
            Rule rule7 = new Rule("https://doi.org/10.1145/3132498.3134268",
                                  new AndCriteria(
                                            new AndCriteria(
                                                new MetricCriteria(CaDETMetric.LCOM, OperationEnum.GREATER_OR_EQUALS, 0.725),
                                                new MetricCriteria(CaDETMetric.WMC, OperationEnum.GREATER_OR_EQUALS, 34)),
                                            new AndCriteria(
                                                new MetricCriteria(CaDETMetric.NAD, OperationEnum.GREATER_OR_EQUALS, 8),
                                                new MetricCriteria(CaDETMetric.NMD, OperationEnum.GREATER_OR_EQUALS, 14))),
                                  SmellType.GOD_CLASS);
            Rule rule9 = new Rule("10.1109/SCET.2012.6342082",
                                    new OrCriteria(
                                      new MetricCriteria(CaDETMetric.CLOC, OperationEnum.GREATER_THAN, 750),
                                      new OrCriteria(new MetricCriteria(CaDETMetric.NAD, OperationEnum.GREATER_THAN, 9),
                                                     new MetricCriteria(CaDETMetric.NMD, OperationEnum.GREATER_THAN, 20))),
                                  SmellType.GOD_CLASS);
            Rule rule10 = new Rule("10.1109/JCSSE.2011.5930141",
                                  new OrCriteria(
                                      new OrCriteria(new MetricCriteria(CaDETMetric.LCOM3, OperationEnum.GREATER_THAN, 0.8),
                                                    new MetricCriteria(CaDETMetric.LCOM4, OperationEnum.GREATER_THAN, 1)),
                                      new OrCriteria(new MetricCriteria(CaDETMetric.NMD, OperationEnum.GREATER_THAN, 20),
                                                    new MetricCriteria(CaDETMetric.NAD, OperationEnum.GREATER_THAN, 20))
                                      ),
                                  SmellType.GOD_CLASS);
            Rule rule11 = new Rule("10.1109/TSE.2011.9",
                                  new OrCriteria(
                                      new MetricCriteria(CaDETMetric.CLOC, OperationEnum.GREATER_THAN, 100),
                                      new MetricCriteria(CaDETMetric.CYCLO, OperationEnum.GREATER_THAN, 20)
                                      ),
                                  SmellType.GOD_CLASS);
            Rule rule12 = new Rule("",
                                  new OrCriteria(
                                      new MetricCriteria(CaDETMetric.DIT, OperationEnum.GREATER_THAN, 5),
                                      new MetricCriteria(CaDETMetric.DCC, OperationEnum.GREATER_THAN, 10)
                                      ),
                                  SmellType.GOD_CLASS);
            //ATFD se u ovom radu računa kao broj atributa kojima je direktno pristupljeno. Da li uzeti u obzir?
            Rule rule13 = new Rule("10.1145/1852786.1852797",
                                  new AndCriteria(
                                            new AndCriteria(
                                                new MetricCriteria(CaDETMetric.ATFD_10, OperationEnum.GREATER_THAN, 5),
                                                new MetricCriteria(CaDETMetric.WMC, OperationEnum.GREATER_OR_EQUALS, 47)),
                                            new MetricCriteria(CaDETMetric.TCC, OperationEnum.LESS_THAN, 0.33)),
                                  SmellType.GOD_CLASS);

            _rules = new List<Rule>();
            _rules.Add(rule1);
            _rules.Add(rule2);
            _rules.Add(rule3);
            _rules.Add(rule4);
            _rules.Add(rule5);
            _rules.Add(rule6);
            _rules.Add(rule7);
            _rules.Add(rule9);
            _rules.Add(rule10);
            _rules.Add(rule11);
            _rules.Add(rule12);
            _rules.Add(rule13);
            _dynamicRules = new List<Rule>();
        }

        private void DefineTopXMetricRules(List<CaDETClass> classes)
        {
            int indexOfSignificantMetricValue = CalculateIndexBasedOnPercentage(classes, 20);
            double atfdThreshold = FindTopXMetricValuesInProject(classes, CaDETMetric.ATFD, indexOfSignificantMetricValue);

            Rule rule1 = new Rule("10.1016/j.jss.2006.10.018",
                                                new AndCriteria(new AndCriteria(new MetricCriteria(CaDETMetric.ATFD, OperationEnum.GREATER_OR_EQUALS, atfdThreshold),
                                                                new MetricCriteria(CaDETMetric.ATFD, OperationEnum.GREATER_THAN, 4)),
                                                new AndCriteria(new MetricCriteria(CaDETMetric.WMC, OperationEnum.GREATER_THAN, 20),
                                                                new MetricCriteria(CaDETMetric.TCC, OperationEnum.LESS_THAN, 0.33))),
                                SmellType.GOD_CLASS);

            double wmcThreshold = FindTopXMetricValuesInProject(classes, CaDETMetric.WMC, 10);

            Rule rule2 = new Rule("10.1109/TOOLS.2001.941671",
                                new OrCriteria(new OrCriteria(new MetricCriteria(CaDETMetric.ATFD, OperationEnum.GREATER_THAN, 3),
                                                                new MetricCriteria(CaDETMetric.WMC, OperationEnum.GREATER_THAN, wmcThreshold)),
                                                new MetricCriteria(CaDETMetric.TCC, OperationEnum.LESS_THAN, 0.33)),
                                SmellType.GOD_CLASS);
            _dynamicRules.Add(rule1);
            _dynamicRules.Add(rule2);
        }

        private double FindTopXMetricValuesInProject(List<CaDETClass> classes, CaDETMetric metric, int indexOfMetricValue)
        {
            List<double> metricValues = classes.Select(c => c.Metrics[metric]).ToList();
            metricValues.Sort();
            return metricValues.Count <= indexOfMetricValue ? metricValues[metricValues.Count - 1] : metricValues[indexOfMetricValue];
        }

        private int CalculateIndexBasedOnPercentage(List<CaDETClass> classes, int percentage)
        {
            return classes.Count * percentage / 100;
        }

        public PartialSmellDetectionReport FindIssues(List<CaDETClass> classes)
        {
            //DefineTopXMetricRules(caDetClassDtoList);
            var partialReport = new PartialSmellDetectionReport();

            foreach(var cadetClass in classes)
            {
                var issues = ApplyRules(cadetClass);
                foreach (var issue in issues.Where(issue => issue != null))
                {
                    partialReport.AddIssue(issue.CodeSnippetId, issue);
                }
            }
            return partialReport;
        }

        private List<Issue> ApplyRules(CaDETClass c)
        {
            List<Issue> issues = _rules.Select(r => r.Validate(c.FullName, c.Metrics)).ToList();
            issues.AddRange(_dynamicRules.Select(r => r.Validate(c.FullName, c.Metrics)).ToList());
            return issues;
        }
    }
}
