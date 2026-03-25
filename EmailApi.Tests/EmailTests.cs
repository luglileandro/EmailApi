using EmailApi.Models;

namespace EmailApi.Tests;
public class EmailTests
{
    [Fact]
    public void Should_Return_Emails_From_Given_City()
    {
        var users = new List<User>
        {
            new User { email = "a@test.com", address = new Address { city = "A" } },
            new User { email = "b@test.com", address = new Address { city = "B" } },
            new User { email = "c@test.com", address = new Address { city = "A" } }
        };

        var result = users
            .Where(u => u.address?.city == "A")
            .Select(u => u.email)
            .ToList();

        Assert.Equal(2, result.Count);
        Assert.Contains("a@test.com", result);
        Assert.Contains("c@test.com", result);
    }

    [Fact]
    public void Should_Return_One_Email_For_Gwenborough()
    {
        var users = new List<User>
        {
            new User { email = "Sincere@april.biz", address = new Address { city = "Gwenborough" } },
            new User { email = "Shanna@melissa.tv", address = new Address { city = "Wisokyburgh" } },
            new User { email = "Nathan@yesenia.net", address = new Address { city = "McKenziehaven" } }
        };

        var result = users
            .Where(u => u.address?.city == "Gwenborough")
            .Select(u => u.email)
            .ToList();

        Assert.Single(result);
        Assert.Equal("Sincere@april.biz", result[0]);
    }

    [Fact]
    public void Should_Return_Empty_List_When_City_Does_Not_Exist()
    {
        var users = new List<User>
        {
            new User { email = "Sincere@april.biz", address = new Address { city = "Gwenborough" } },
            new User { email = "Shanna@melissa.tv", address = new Address { city = "Wisokyburgh" } }
        };

        var result = users
            .Where(u => u.address?.city == "City that does not exist")
            .Select(u => u.email)
            .ToList();

        Assert.Empty(result);
    }

    [Fact]
    public void Should_Ignore_User_With_Null_Address()
    {
        var users = new List<User>
        {
            new User { email = "noaddress@test.com", address = null! },
            new User { email = "valid@test.com", address = new Address { city = "Gwenborough" } }
        };

        var result = users
            .Where(u => u.address?.city == "Gwenborough")
            .Select(u => u.email)
            .ToList();

        Assert.Single(result);
        Assert.Equal("valid@test.com", result[0]);
    }
}