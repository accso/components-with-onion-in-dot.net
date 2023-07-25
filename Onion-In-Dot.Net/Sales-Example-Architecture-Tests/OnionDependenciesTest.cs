using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using System;
using System.Linq;
using Xunit;

namespace Sales_Example_Architecture_Tests
{
    public class OnionDependenciesTest
    {
        private const string PACKAGE_PREFIX = "Accso.Ecommerce.Onion.Sales.";

        private readonly IObjectProvider<IType> ApiLayer = ArchRuleDefinition.Types().That()
               .ResideInNamespace(CreateNamespaceRegExpr("Api.*"))
               .As("API Layer");

        private readonly IObjectProvider<IType> ApplicationLayer = ArchRuleDefinition.Types().That()
            .ResideInNamespace(CreateNamespaceRegExpr("Core.Application.*"), true)
                        .As("Application Layer");

        private readonly IObjectProvider<IType> DomainLayer = ArchRuleDefinition.Types().That()
           .ResideInNamespace(CreateNamespaceRegExpr("Core.Domain"), true)
                       .As("Domain Layer");

        private readonly IObjectProvider<IType> InfrastructureLayer = ArchRuleDefinition.Types().That()
          .ResideInNamespace(CreateNamespaceRegExpr(".Infrastructure.*"), true)
                      .As("Infra Layer");

        private readonly IObjectProvider<IType> UiLayer = ArchRuleDefinition.Types().That()
          .ResideInNamespace(CreateNamespaceRegExpr("UI"), true)
                      .As("UI Layer");

        private readonly ArchUnitNET.Domain.Architecture EcommerceArchitecture;

        public OnionDependenciesTest()
        {
            EcommerceArchitecture = LoadAssembliesUnderConsideration();
        }

        private ArchUnitNET.Domain.Architecture LoadAssembliesUnderConsideration()
            => new ArchLoader()
                .LoadAssemblies(GetAssembliesForConsideration()).Build();

        private System.Reflection.Assembly[] GetAssembliesForConsideration() => AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => IsAssemblyConsidered(assembly))
            .ToArray();

        private readonly Func<System.Reflection.Assembly, bool> IsAssemblyConsidered = delegate (System.Reflection.Assembly assemblyUnderEvaluation)
        {
            return assemblyUnderEvaluation.GetTypes()
            .Where(type => (type.IsInterface || type.IsClass)
            && !String.IsNullOrEmpty(type.Namespace)
            && type.Namespace.StartsWith(PACKAGE_PREFIX))
            .ToList().Count > 0;
        };

        [Fact]
        public void Test_Onion_LayerDomainMustOnlyDependOnItself()
        {
            //assign
            IArchRule onionRule = ArchRuleDefinition.Types().That()
                 .Are(DomainLayer).Should().NotDependOnAny(ApplicationLayer)
                 .AndShould().NotDependOnAny(InfrastructureLayer)
                 .AndShould().NotDependOnAny(ApiLayer)
                 .AndShould().NotDependOnAny(UiLayer)
                 .Because("Domain layer must only depend on itself");

            //when + then
            onionRule.Check(EcommerceArchitecture);
        }

        [Fact]
        public void Test_Onion_ApplicationLayerMustOnlyDependOnDomain()
        {
            //assign
            IArchRule onionRule = ArchRuleDefinition.Types().That()
                 .Are(ApplicationLayer).Should().NotDependOnAny(InfrastructureLayer)
                 .AndShould().NotDependOnAny(ApiLayer)
                 .AndShould().NotDependOnAny(UiLayer)
                 .Because("Application layer must only depend on domain layer");

            //when + then
            onionRule.Check(EcommerceArchitecture);
        }

        [Fact]
        public void Test_Onion_ApiLayerMustOnlyDependOnApplicationAndDomain()
        {
            //assign
            IArchRule onionRule = ArchRuleDefinition.Types().That()
                 .Are(ApiLayer).Should().NotDependOnAny(InfrastructureLayer)
                 .AndShould().NotDependOnAny(UiLayer)
                 .Because("Api layer must only depend on domain and application layer");

            //when + then
            onionRule.Check(EcommerceArchitecture);
        }

        [Fact]
        public void Test_Onion_InfrastructureLayerMustOnlyDependOnApplicationAndDomain()
        {
            //assign
            IArchRule onionRule = ArchRuleDefinition.Types().That()
                 .Are(InfrastructureLayer).Should().NotDependOnAny(ApiLayer)
                 .AndShould().NotDependOnAny(UiLayer)
                 .Because("Infrastructure layer must only depend on domain and application layer");

            //when + then
            onionRule.Check(EcommerceArchitecture);
        }

        [Fact]
        public void Test_Onion_UILayerMustOnlyDependOnApplicationAndDomain()
        {
            //assign
            IArchRule onionRule = ArchRuleDefinition.Types().That()
                 .Are(UiLayer).Should().NotDependOnAny(ApiLayer)
                 .AndShould().NotDependOnAny(InfrastructureLayer)
                 .Because("UI layer must only depend on domain and application layer");

            //when + then
            onionRule.Check(EcommerceArchitecture);
        }

        private static string CreateNamespaceRegExpr(string lastPartOfNamespace)
        {
            return string.Join("*.", PACKAGE_PREFIX, lastPartOfNamespace);
        }
    }
}