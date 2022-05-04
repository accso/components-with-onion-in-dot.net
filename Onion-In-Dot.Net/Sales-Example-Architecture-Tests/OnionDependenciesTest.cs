using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Sales_Example_Architecture_Tests
{
    public class OnionDependenciesTest
    {
        private static readonly string PackagePrefix = "Accso.Ecommerce.Onion.Sales.";
        private static readonly Func<Assembly, bool> IsAssemblyConsidered = delegate (Assembly assemblyUnderEvaluation)
        {
            return assemblyUnderEvaluation.GetTypes()
            .Where(type => (type.IsInterface || type.IsClass)
            && !String.IsNullOrEmpty(type.Namespace)
            && type.Namespace.StartsWith(PackagePrefix))
            .ToList().Count > 0;
        };
        private static readonly List<Assembly> assembliesToConsiderForTest = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => IsAssemblyConsidered(assembly))
            .ToList();
        private static readonly ArchUnitNET.Domain.Architecture onionSalesArchitecture = new ArchLoader()
                .LoadAssemblies(assembliesToConsiderForTest.ToArray()).Build();

        [Fact]
        public void Test_ShouldBeTrue_BecauseAPIMustOnlyDependOnApplication()
        {
            //given
            var apiLayer = ArchRuleDefinition.Types().That()
                .ResideInNamespace(CreateNamespaceRegExpr("Api.* "), true)
                .As("API Layer");
            var applicationLayer = ArchRuleDefinition.Types().That()
                .ResideInNamespace(CreateNamespaceRegExpr("Core.Application.*"), true)
                            .As("Application Layer");            
            IArchRule onionAPIRule = ArchRuleDefinition.Types().That()
               .Are(apiLayer).Should().OnlyDependOn(applicationLayer)
               .Because("Only this allowed");
            //when + then
           onionAPIRule.Check(onionSalesArchitecture);           
        }

        [Fact]
        public void Test_ShouldBeTrue_BecauseApplicationMustOnlyDependOnDomain()
        {
            //given            
            var applicationLayer = ArchRuleDefinition.Types().That()
                //the test fails if there is no whitespace after the asteriks. Please don't ask me why. :-)
                .ResideInNamespace(CreateNamespaceRegExpr("Core.Application.* "), true)
                            .As("Application Layer");
            var domainLayer = ArchRuleDefinition.Types().That()
                .ResideInNamespace(CreateNamespaceRegExpr("Core.Domain.*"), true)
                .As("Domain Layer");
            IArchRule onionApplicationRule = ArchRuleDefinition.Types().That()
               .Are(applicationLayer).Should().OnlyDependOn(domainLayer)
               .Because("Only this allowed");
            //when + then
           onionApplicationRule.Check(onionSalesArchitecture);
        }

        [Fact]
        public void Test_ShouldBeTrue_BecauseDomainMustOnlyDependOnDomain()
        {
            //given              
            var domainLayer = ArchRuleDefinition.Types().That()
                .ResideInNamespace(CreateNamespaceRegExpr("Core.Domain.*"), true)
                .As("Domain Layer");
            IArchRule onionDomainRule = ArchRuleDefinition.Types().That()
               .Are(domainLayer).Should().OnlyDependOn(domainLayer)
               .Because("Only this allowed");
            //when + then
            onionDomainRule.Check(onionSalesArchitecture);
        }

        [Fact]
        public void Test_ShouldBeTrue_BecausePersistenceMustOnlyDependOnApplication()
        {
            //given
            var persistenceLayer = ArchRuleDefinition.Types().That()
               .ResideInNamespace(CreateNamespaceRegExpr("Infrastructure.Persistence.* "), true)
               .As("Domain Layer");
            var applicationLayer = ArchRuleDefinition.Types().That()
                .ResideInNamespace(CreateNamespaceRegExpr("Core.Application.* "), true)
                            .As("Application Layer");           
            IArchRule onionPersistenceRule = ArchRuleDefinition.Types().That()
               .Are(persistenceLayer).Should().OnlyDependOn(applicationLayer)
               .Because("Only this allowed");
            //when + then
            onionPersistenceRule.Check(onionSalesArchitecture);
        }

        [Fact]
        public void Test_ShouldBeTrue_BecauseMessagingMustOnlyDependOnApplication()
        {
            //given
            var messagingLayer = ArchRuleDefinition.Types().That()
               .ResideInNamespace(CreateNamespaceRegExpr("Infrastructure.Messaging.* "), true)
               .As("Domain Layer");
            var applicationLayer = ArchRuleDefinition.Types().That()
                .ResideInNamespace(CreateNamespaceRegExpr("Core.Application.*"), true)
                            .As("Application Layer");
            IArchRule onionMessagingRule = ArchRuleDefinition.Types().That()
               .Are(messagingLayer).Should().OnlyDependOn(applicationLayer)
               .Because("Only this allowed");
            //when + then
            onionMessagingRule.Check(onionSalesArchitecture);
        }

        private static string CreateNamespaceRegExpr(string lastPartOfNamespace)
        {
            return string.Join("*.", PackagePrefix, lastPartOfNamespace);
        }


    }
}
