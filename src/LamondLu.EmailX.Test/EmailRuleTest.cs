using LamondLu.EmailX.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LamondLu.EmailX.Test
{
    [TestClass]
    public class EmailRuleTest
    {
        [TestMethod]
        public void TestEmailRule_SingleMatchedCritera()
        {
            var criteras = new List<MatchCritera>() {
                new MatchCritera {
                    Condition = Domain.Enum.MatchCondition.Contain,
                    Field = Domain.Enum.MatchField.Subject,
                    Value = "Lamond" }
            };

            var classifyRule = new ClassifyRule();
            classifyRule.Criteras = new MatchCriteraCollection(criteras);

            var result = classifyRule.Match(new Email("id", 0, 0) { Subject = "Lamond Lu" });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEmailRule_TwoCritera_OneMatched()
        {
            var criteras = new List<MatchCritera>() {
                new MatchCritera {
                    Condition = Domain.Enum.MatchCondition.Contain,
                    Field = Domain.Enum.MatchField.Subject,
                    Value = "Lamond" },
                new MatchCritera {
                    Operator = Domain.Enum.MatchOperator.OR,
                    Condition = Domain.Enum.MatchCondition.Contain,
                    Field = Domain.Enum.MatchField.Subject,
                    Value = "xxx" }
            };

            var classifyRule = new ClassifyRule();
            classifyRule.Criteras = new MatchCriteraCollection(criteras);

            var result = classifyRule.Match(new Email("id", 0, 0) { Subject = "Lamond Lu" });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEmailRule_ThreeCritera_OneMatched()
        {
            var criteras = new List<MatchCritera>() {
                new MatchCritera {
                    Condition = Domain.Enum.MatchCondition.Contain,
                    Field = Domain.Enum.MatchField.Subject,
                    Value = "Lamond" },
                new MatchCritera {
                    Operator = Domain.Enum.MatchOperator.OR,
                    Condition = Domain.Enum.MatchCondition.Contain,
                    Field = Domain.Enum.MatchField.Subject,
                    Value = "xxx" },
                new MatchCritera {
                    Operator = Domain.Enum.MatchOperator.AND,
                    Condition = Domain.Enum.MatchCondition.Contain,
                    Field = Domain.Enum.MatchField.Subject,
                    Value = "xxx1" }
            };

            var classifyRule = new ClassifyRule();
            classifyRule.Criteras = new MatchCriteraCollection(criteras);

            var result = classifyRule.Match(new Email("id", 0, 0) { Subject = "Lamond Lu" });

            Assert.IsTrue(result);
        }
    }
}
