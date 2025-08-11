using System.Text;

namespace TemplateProject.Core.Extension;

public static class StringExtension
{
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string Chars = Lowercase + Uppercase + Digits;

    /// <summary>
    /// 生成随机字符串
    /// </summary>
    public static string GenerateRandomString(this string prefix, int length = 9)
    {
        var random = new Random();
        var stringBuilder = new StringBuilder(length);

        for (var i = 0; i < length; i++)
        {
            var randomIndex = random.Next(Chars.Length);
            stringBuilder.Append(Chars[randomIndex]);
        }

        return prefix + stringBuilder;
    }
    
    /// <summary>
    /// 生成随机数字后缀
    /// </summary>
    public static string GenerateNumberSuffix(this string str, int length = 4)
    {
        var random = new Random();
        var suffix = random.Next(0, 10000);
        return suffix.ToString("D" + length);
    }
    
    /// <summary>
    /// 生成随机密码, 包含大写字母、小写字母、数字
    /// </summary>
    /// <param name="length">字符最低为3长度</param>
    /// <returns></returns>
    public static string GenerateRandomPassword(int length = 12)
    {
        if (length < 3)
            length = 3;

        var random = new Random();
        var passwordChars = new char[length];

        passwordChars[0] = Uppercase[random.Next(Uppercase.Length)];
        passwordChars[1] = Lowercase[random.Next(Lowercase.Length)];
        passwordChars[2] = Digits[random.Next(Digits.Length)];

        for (var i = 3; i < length; i++)
        {
            passwordChars[i] = Chars[random.Next(Chars.Length)];
        }

        return Shuffle(passwordChars, random);
    }
    
    /// <summary>
    /// 随机打乱字符数组
    /// </summary>
    private static string Shuffle(char[] array, Random random)
    {
        for (var i = array.Length - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }

        return new string(array);
    }
}