namespace Macabresoft.AvaloniaEx.Tests.Converters;

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

[TestFixture]
public class EnumTypeToValuesConverterTests {
    [Test]
    [Category("Unit Tests")]
    public static void Convert_ShouldIncludeZero_WhenNotFlags() {
        var converter = new EnumTypeToValuesConverter();
        var values = ((IEnumerable<object>)converter.Convert(typeof(TestEnum), null, null, null)).Cast<TestEnum>().ToList();

        using (new AssertionScope()) {
            values.Count.Should().Be(4);
            values.Should().Contain(TestEnum.Value0);
        }
    }
    
    [Test]
    [Category("Unit Tests")]
    public static void Convert_ShouldNotIncludeZero_WhenFlags() {
        var converter = new EnumTypeToValuesConverter();
        var values = ((IEnumerable<object>)converter.Convert(typeof(TestFlagsEnum), null, null, null)).Cast<TestFlagsEnum>().ToList();

        using (new AssertionScope()) {
            values.Count.Should().Be(3);
            values.Should().NotContain(TestFlagsEnum.Value0);
        }
    }

    [Flags]
    private enum TestFlagsEnum : byte {
        Value0 = 0,
        Value1 = 1,
        Value2 = 2,
        Value3 = 4
    }
    
    private enum TestEnum {
        Value0 = 0,
        Value1 = 1,
        Value2 = 2,
        Value3 = 3
    }
}