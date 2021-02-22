using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Service.Helpers.Interfaces;

namespace Service.Helpers
{
    public class SearchEngineHelper : ISearchEngineHelper
    {
        public List<Expression<Func<DomainModels.Show, bool>>> GetExpressionBasedOnText(string text)
        {
            if (text == null)
            {
                return null;
            }

            var expressions = new List<Expression<Func<DomainModels.Show, bool>>>();
            if (new Regex(@"at least [1-5] star[s]?").IsMatch(text))
            {
                var regexPart = Regex.Match(text, @"at least [1-5] star[s]?").Value;
                var stringNum = Regex.Match(regexPart, @"\d+").Value;

                if (int.TryParse(stringNum, out int num))
                {
                    expressions.Add(show => (int)show.AverageRate >= num);
                }
            }
            else if (new Regex(@"[1-5] star[s]?").IsMatch(text)) {
                var stringNum = Regex.Match(text, @"\d+").Value;

                if (int.TryParse(stringNum, out int num))
                {
                    expressions.Add(show => (int)show.AverageRate == num);
                }
            }

            if (new Regex(@"after \d{4}").IsMatch(text))
            {
                var regexPart = Regex.Match(text, @"after \d+").Value;
                var stringNum = Regex.Match(regexPart, @"\d{4}").Value;

                if (int.TryParse(stringNum, out int year))
                {
                    expressions.Add(show => show.ReleaseDate.Year > year);
                }
            }

            if (new Regex(@"older than \d years").IsMatch(text))
            {
                var regexPart = Regex.Match(text, @"older than \d years").Value;
                var stringNum = Regex.Match(regexPart, @"\d+").Value;

                if (int.TryParse(stringNum, out int olderThanYears))
                {
                    var dateBeforeSpecifiedYears = DateTime.Now.AddYears(-olderThanYears);
                    expressions.Add(show => show.ReleaseDate < dateBeforeSpecifiedYears);
                }
            }

            return expressions;
        }
    }
}
