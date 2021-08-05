﻿using CodeModel;
using CodeModel.CaDETModel;
using CodeModel.CaDETModel.CodeItems;
using DataSetExplorer.DataSetBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataSetExplorer.DataSetBuilder
{
    internal class CaDETToDataSetProjectBuilder
    {
        private readonly string _projectAndCommitUrl;
        private readonly string _projectName;

        private readonly CaDETProject _cadetProject;
        private int _percentileOfProjectCovered = 100;

        private readonly bool _includeClasses;
        private bool _randomizeClassList;
        
        private readonly bool _includeMembers;
        private bool _randomizeMemberList;
        private CaDETMemberType[] _acceptedMemberTypes = {CaDETMemberType.Constructor, CaDETMemberType.Method};
        private int _minimumELOC;
        private int _minimumNMD;
        private int _minimumNAD;


        internal CaDETToDataSetProjectBuilder(string projectAndCommitUrl, string projectName, string projectPath, LanguageEnum language, bool includeClasses, bool includeMembers)
        {
            _projectAndCommitUrl = projectAndCommitUrl;
            _projectName = projectName;
            _cadetProject = new CodeModelFactory(language).CreateProjectWithCodeFileLinks(projectPath);
            _includeClasses = includeClasses;
            _includeMembers = includeMembers;
        }

        internal CaDETToDataSetProjectBuilder(string projectAndCommitUrl, string projectName, string projectPath): this(projectAndCommitUrl, projectName, projectPath, LanguageEnum.CSharp, true, true) { }

        internal CaDETToDataSetProjectBuilder SetProjectExtractionPercentile(int percentile)
        {
            _percentileOfProjectCovered = percentile;
            return this;
        }
        
        internal CaDETToDataSetProjectBuilder RandomizeClassSelection()
        {
            ValidateClassesIncluded();
            _randomizeClassList = true;
            return this;
        }

        /// <summary>Final dataset will include classes that have minimumNMD or minimumNAD</summary>
        /// <param name="minimumNMD">Minimum number of methods.</param>
        /// <param name="minimumNAD">Minimum number of fields and properties with implicit fields.</param>
        internal CaDETToDataSetProjectBuilder IncludeClassesWith(int minimumNMD, int minimumNAD)
        {
            ValidateClassesIncluded();
            _minimumNMD = minimumNMD;
            _minimumNAD = minimumNAD;
            return this;
        }

        private void ValidateClassesIncluded()
        {
            if (!_includeClasses) throw new InvalidOperationException("Classes are not included.");
        }

        internal CaDETToDataSetProjectBuilder IncludeMemberTypes(CaDETMemberType[] acceptedTypes)
        {
            ValidateMembersIncluded();
            _acceptedMemberTypes = acceptedTypes;
            return this;
        }

        private void ValidateMembersIncluded()
        {
            if (!_includeMembers) throw new InvalidOperationException("Members are not included.");
        }

        internal CaDETToDataSetProjectBuilder RandomizeMemberSelection()
        {
            ValidateMembersIncluded();
            _randomizeMemberList = true;
            return this;
        }

        internal CaDETToDataSetProjectBuilder IncludeMembersWith(int minimumELOC)
        {
            ValidateMembersIncluded();
            _minimumELOC = minimumELOC;
            return this;
        }

        internal DataSetProject Build()
        {
            var builtDataSetProject = new DataSetProject(_projectName, _projectAndCommitUrl);
            if (_includeClasses) builtDataSetProject.AddInstances(BuildClasses());
            if (_includeMembers) builtDataSetProject.AddInstances(BuildMembers());
            return builtDataSetProject;
        }

        private List<DataSetInstance> BuildClasses()
        {
            var cadetClasses = _cadetProject.Classes.Where(
                c => c.Metrics[CaDETMetric.NAD] >= _minimumNAD || c.Metrics[CaDETMetric.NMD] >= _minimumNMD).ToList();
            if(_randomizeClassList) ShuffleList(cadetClasses);
            if(_percentileOfProjectCovered < 100) cadetClasses = cadetClasses.Take(DetermineNumberOfInstances(_cadetProject.Classes.Count)).ToList();
            return CaDETToDataSetProjectClasses(cadetClasses);
        }

        private int DetermineNumberOfInstances(int totalNumber)
        {
            return totalNumber * _percentileOfProjectCovered / 100;
        }

        private List<DataSetInstance> CaDETToDataSetProjectClasses(List<CaDETClass> cadetClasses)
        {
            return cadetClasses.Select(c => new DataSetInstance(
                c.FullName, GetCodeUrl(c.FullName), _projectAndCommitUrl, SnippetType.Class, _cadetProject.GetMetricsForCodeSnippet(c.FullName)
            )).ToList();
        }

        private string GetCodeUrl(string snippetId)
        {
            _cadetProject.CodeLinks.TryGetValue(snippetId, out var codeLink);
            return _projectAndCommitUrl + codeLink.FileLocation + "#L" + codeLink.StartLoC + "-L" + codeLink.EndLoC;
        }

        private static void ShuffleList<T>(IList<T> list)
        {
            var rnd = new Random();
            for (var i = 0; i < list.Count - 1; i++)
            {
                int randomSwapLocation = rnd.Next(i, list.Count);
                var temp = list[i];
                list[i] = list[randomSwapLocation];
                list[randomSwapLocation] = temp;
            }
        }

        private List<DataSetInstance> BuildMembers()
        {
            var cadetMembers = new List<CaDETMember>();
            var allMembers= _cadetProject.Classes.SelectMany(c => c.Members).ToList();
            cadetMembers.AddRange(allMembers.Where(
                    m => _acceptedMemberTypes.Contains(m.Type) && m.Metrics[CaDETMetric.MELOC] >= _minimumELOC));
            if (_randomizeMemberList) ShuffleList(cadetMembers);
            if (_percentileOfProjectCovered < 100) cadetMembers = cadetMembers.Take(DetermineNumberOfInstances(allMembers.Count)).ToList();

            return CaDETToDataSetFunction(cadetMembers);
        }

        private List<DataSetInstance> CaDETToDataSetFunction(List<CaDETMember> cadetMembers)
        {
            return cadetMembers.Select(m => new DataSetInstance(
                m.Signature(), GetCodeUrl(m.Signature()), _projectAndCommitUrl, SnippetType.Function, _cadetProject.GetMetricsForCodeSnippet(m.Signature())
            )).ToList();
        }
    }
}
