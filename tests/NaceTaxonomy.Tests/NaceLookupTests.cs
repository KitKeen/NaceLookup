using Nace;
using Xunit;

public class NaceLookupTests
{
    [Fact]
    public void Count_Returns651() =>
        Assert.Equal(651, NaceLookup.Count);

    [Theory]
    [InlineData("01.11", "Growing of cereals, other than rice, leguminous crops and oil seeds", "A")]
    [InlineData("64.19", "Other monetary intermediation", "L")]
    [InlineData("62.10", "Computer programming activities", "K")]
    [InlineData("66.19", "Other activities auxiliary to financial services, except insurance and pension funding", "L")]
    public void Get_KnownCode_ReturnsCorrectClass(string code, string expectedDesc, string expectedSection)
    {
        var result = NaceLookup.Get(code);
        Assert.NotNull(result);
        Assert.Equal(code, result.Code);
        Assert.Equal(expectedSection, result.Section);
        Assert.Contains(expectedDesc.Substring(0, 20), result.Description);
    }

    [Theory]
    [InlineData("0111")]   // no dot
    [InlineData("1.11")]   // missing leading zero
    public void Get_AlternativeFormats_ReturnsResult(string code)
    {
        var result = NaceLookup.Get(code);
        Assert.NotNull(result);
        Assert.Equal("01.11", result.Code);
    }

    [Fact]
    public void Get_UnknownCode_ReturnsNull() =>
        Assert.Null(NaceLookup.Get("99.99"));

    [Fact]
    public void Get_NullOrEmpty_ReturnsNull()
    {
        Assert.Null(NaceLookup.Get(null!));
        Assert.Null(NaceLookup.Get(""));
    }

    [Fact]
    public void Get_CaseInsensitive()
    {
        Assert.NotNull(NaceLookup.Get("01.11"));
        Assert.NotNull(NaceLookup.Get("01.11".ToUpper()));
    }

    [Fact]
    public void TryGet_KnownCode_ReturnsTrueAndClass()
    {
        Assert.True(NaceLookup.TryGet("64.19", out var c));
        Assert.NotNull(c);
    }

    [Fact]
    public void GetBySection_K_ReturnsTechClasses()
    {
        var classes = NaceLookup.GetBySection("K").ToList();
        Assert.NotEmpty(classes);
        Assert.All(classes, c => Assert.Equal("K", c.Section));
    }

    [Fact]
    public void All_Returns651Items() =>
        Assert.Equal(651, NaceLookup.All.Count());
}
