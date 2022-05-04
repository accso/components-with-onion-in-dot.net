using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Slices;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sales_Example_Architecture_Tests
{
    public class NoCyclesTest
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
        public void Test_ShouldBeTrue_WhenNoDependencyCycleExists()
        {
            //given
            IArchRule freeOfCyclesRule = SliceRuleDefinition.Slices()
               .Matching(PackagePrefix + "(**)")
               .Should()
               .BeFreeOfCycles();
            //when
            var validationResult = freeOfCyclesRule.HasNoViolations(onionSalesArchitecture);
            //then
            Assert.True(validationResult);
        }


    }
}
