using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Accso.Ecommerce.Architecture.Test
{
    public class OnionDependenciesTest
    {

        private static readonly string PACKAGE_PREFIX = "Accso.Ecommerce.";
        private static readonly Func<Assembly, bool> IsAssemblyConsidered = delegate (Assembly assemblyUnderEvaluation)
        {
            return assemblyUnderEvaluation.GetTypes()
            .Where(type => (type.IsInterface || type.IsClass)
            && !String.IsNullOrEmpty(type.Namespace)
            && type.Namespace.StartsWith(PACKAGE_PREFIX))
            .ToList().Count > 0;
        };
        private static readonly List<Assembly> my = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => IsAssemblyConsidered(assembly))
            .ToList();
        private static readonly ArchUnitNET.Domain.Architecture eCommerceArchitecture = new ArchLoader()
                .LoadAssemblies(my.ToArray()).Build();


       [Fact]
        public void Test_Onion_Architecture_Inside_One_Component_Using_Layers()
        {
            //given            
            var apiLayer = ArchRuleDefinition.Types().That()
                .ResideInNamespace(CreateNamespaceRegExpr("Api.*"))
                .As("API Layer");
            var applicationLayer = ArchRuleDefinition.Types().That()
                .ResideInNamespace(CreateNamespaceRegExpr("Core.Application.*"), true)
                            .As("Application Layer");
            var domainLayer = ArchRuleDefinition.Types().That()
               .ResideInNamespace(CreateNamespaceRegExpr("Core.Domain"), true)
                           .As("Domain Layer");
            var infrastructureLayer = ArchRuleDefinition.Types().That()
              .ResideInNamespace(CreateNamespaceRegExpr(".Infrastructure.*"), true)
                          .As("Infra Layer");
            var uiLayer = ArchRuleDefinition.Types().That()
              .ResideInNamespace(CreateNamespaceRegExpr("UI"), true)
                          .As("UI Layer");
            var common = ArchRuleDefinition.Types().That()
              .ResideInNamespace(CreateNamespaceRegExpr("Common"), true)
                          .As("Common");
            IArchRule onionRule = ArchRuleDefinition.Types().That()
                .Are(domainLayer).Should().OnlyDependOn(domainLayer)
                .And()
                .Types().That().Are(applicationLayer).Should().OnlyDependOn(domainLayer)
                .And()
                .Types().That().Are(uiLayer).Should().OnlyDependOn(applicationLayer)
                .And()
                .Types().That().Are(infrastructureLayer).Should().OnlyDependOn(applicationLayer)
                .And()
                .Types().That().Are(apiLayer).Should().OnlyDependOn(applicationLayer)                
                .And()
                .Types().That().Are(uiLayer).Should().OnlyDependOn(applicationLayer)                
                .Because("It is forbidden");

            //when + then
            onionRule.Check(eCommerceArchitecture);
        }

        /// <summary>
        /// internal class to represents a layer with name and corresponding namespace
        /// </summary>
        class Layer
        {
            public Layer(string name, string namespaceOfTypes)
            {
                this.Name = name;
                this.Namespace = namespaceOfTypes;
            }

            public string Name { get; }
            public string Namespace { get; }
        }

        private static string CreateNamespaceRegExpr(string lastPartOfNamespace)
        {
            return string.Join("*.", PACKAGE_PREFIX, lastPartOfNamespace);
        }

    } 
}
