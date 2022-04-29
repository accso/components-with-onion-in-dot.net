using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Slices;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Accso.Ecommerce.Architecture.Test
{
    public class CyclesAndComponentDependenciesTest
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
        public void Test_Slices_Are_Free_Of_Cycles()
        {
            IArchRule freeOfCyclesRule = SliceRuleDefinition.Slices()
                .Matching(PACKAGE_PREFIX + "(*)")
                .Should()
                .BeFreeOfCycles();
            freeOfCyclesRule.Check(eCommerceArchitecture);
            //fails because of Sales -> Shipping -> Sales

        }

        // -------------------------------------------------------------------------------------

        private class Component
        {
            public Component(string name, string package)
            {
                this.Name = name;
                this.Package = package;
            }

            public string Name { get; }
            public string Package { get; }
        }

        [Fact]
        public void Test_Component_Have_Defined_Dependencies()
        {
            // arrange
            var billing = new Component("Billing", "Accso.Ecommerce.Billing.*");
            var common = new Component("Common", "Accso.Ecommerce.Common.*");
            var sales = new Component("Sales", "Accso.Ecommerce.Sales.*");
            var shipping = new Component("Shipping", "Accso.Ecommerce.Shipping.*");
            var warehouse = new Component("Wahrehouse", "Accso.Ecommerce.Warehouse.*");

            //act and assert
            CheckDependencies(billing, common, shipping, sales);
            CheckDependencies(sales, common, warehouse, shipping);
            CheckDependencies(shipping, common, sales);
            CheckDependencies(warehouse, common);
            CheckDependencies(common);
        }

        private static void CheckDependencies(Component from, params Component[] to)
        {
            var toPackages = to.Select(component => component.Package).ToList();
            toPackages.Add(from.Package); // add self as allowed dependency
            var namespaceExpression = CreateNamespaceRegExpr(toPackages.ToArray());
            ArchRuleDefinition.Types()
                .That().ResideInNamespace(from.Package, true)
                .Should().OnlyDependOnTypesThat()
                .ResideInNamespace(namespaceExpression, true)
                .Check(eCommerceArchitecture);
        }
        private static string CreateNamespaceRegExpr(params string[] to) => string.Join('|', (object[])to);
    }
}
