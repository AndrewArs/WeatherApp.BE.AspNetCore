using Infrastructure.Services;

namespace UnitTests.Tests;

public class SlugTests
{
    public SlugService SlugService;

    public SlugTests()
    {
        SlugService = new SlugService();
    }

    [Theory]
    [InlineData("Tomorrow io", "tomorrow-io")]
    [InlineData("Tomorrow.io", "tomorrow-io")]
    [InlineData("Tomorrow/io \\ some_text crème brûlée", "tomorrow-io-some-text-creme-brulee")]
    [InlineData("Tomorrow.io 2 (another request)", "tomorrow-io2-another-request")]
    [InlineData(null, null)]
    public void Slugify(string value, string expectedResult)
    {
        var result = SlugService.Slugify(value);

        result.Should().Be(expectedResult);
    }
}