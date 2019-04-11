using System.Collections.Generic;
using System.Linq;
using Aircon.Business;
using Aircon.Common.Dto;
using Aircon.Tests.MockDataAccess;
using Moq;
using NUnit.Framework;

namespace Aircon.Tests
{
    [TestFixture]
    public class AddNetPresentValue
    {
        private readonly NetPresentValueBusiness _netPresentValueBusiness;
        private readonly MockDbContext _moqDbContext;

        public AddNetPresentValue()
        {
            _moqDbContext = new MockDbContext();
            _netPresentValueBusiness = new NetPresentValueBusiness(_moqDbContext.MockDbContextDataAccess);
        }

        [Test]
        public void Calculate()
        {
            var input = new NpvCalculateParameter
            {
                Amount = 1000,
                CashFlows = new List<CashFlowInputDto>()
                {
                    new CashFlowInputDto{ Period = 1, Amount = 500},
                    new CashFlowInputDto{ Period = 2, Amount = 300},
                    new CashFlowInputDto{ Period = 3, Amount = 800}
                },
                DiscountIncrement = 0.25,
                UpperBoundDiscount = 3,
                LowerBoundDiscount = 1
            };

            var result = _netPresentValueBusiness.Calculate(input);
        }

        [Test]
        public void Calculate_SingleDiscountRate()
        {
            var input = new NpvCalculateParameter
            {
                Amount = 50000,
                CashFlows = new List<CashFlowInputDto>()
                {
                    new CashFlowInputDto{ Period = 1, Amount = 100000},
                    new CashFlowInputDto{ Period = 2, Amount = 200000},
                },
                DiscountIncrement = 0,
                UpperBoundDiscount = 1,
                LowerBoundDiscount = 1
            };
            var expectedResult = 245069.110871483M;

            var result = _netPresentValueBusiness.Calculate(input).ToList();
            var actualResult = result[0].NetPresentValue;

            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void Sample()
        {
            var test = _moqDbContext.MockDbContextDataAccess.CashFlowInputDataAccess.All().Result;
        }
    }
}
