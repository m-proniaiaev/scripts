using System;
using System.Text;
using System.Globalization;
					
public class Program
{
	public static void Main()
	{
		var ch = "Müller";
		var t = "Muller";
		Console.WriteLine("default: " + t.Equals(ch));
		var norm = RemoveDiacritics(ch);
		Console.WriteLine("normalized " + t.Equals(norm));
		var oTest = "o";
		var oc = "ø";
		Console.WriteLine(CultureAwareContains(oc, oTest, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase));
	}
	
	static string RemoveDiacritics(string text)
    {
        if (text == null)
            return null;

        // Normalize to FormD (decomposed)
        string normalized = text.Normalize(NormalizationForm.FormD);

        // Remove diacritical marks
        StringBuilder stringBuilder = new StringBuilder();
        foreach (char c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        // Return as FormC (composed)
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
	
	static bool CultureAwareContains(string source, string toCheck, CultureInfo culture, CompareOptions options)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(toCheck))
            return false;

        for (int i = 0; i <= source.Length - toCheck.Length; i++)
        {
            // Compare substring with the same length as 'toCheck'
            if (string.Compare(source, i, toCheck, 0, toCheck.Length, culture, options) == 0)
            {
                return true;
            }
        }
        return false;
    }
}
