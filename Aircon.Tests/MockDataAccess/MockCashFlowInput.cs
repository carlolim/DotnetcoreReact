using Aircon.Common;
using Aircon.DataAccess;
using Aircon.DataAccess.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Tests.MockDataAccess
{
    public class MockCashFlowInput
    {
        public readonly ICashFlowInputDataAccess MockCashFlowInputDataAccess;
        private readonly Mock<ICashFlowInputDataAccess> _moqCashFlowInputDataAccess = new Mock<ICashFlowInputDataAccess>();
        public MockCashFlowInput()
        {
            var data = SetupData();

            _moqCashFlowInputDataAccess.Setup(mda => mda.All()).Returns(Task.FromResult(data));
            _moqCashFlowInputDataAccess.Setup(mda => mda.ById(It.IsAny<int>())).Returns((int i) => Task.FromResult(data.FirstOrDefault(m => m.CashFlowInputId == i)));
            _moqCashFlowInputDataAccess.Setup(mda => mda.ByTransactionInputId(It.IsAny<int>())).Returns((int i) => Task.FromResult(data.Where(m => m.TransactionInputId == i)));
            _moqCashFlowInputDataAccess.Setup(mda => mda.Insert(It.IsAny<CashFlowInput>())).Returns((CashFlowInput target) =>
            {
                if (target.CashFlowInputId.Equals(default(int)))
                {
                    target.Amount = 400;
                    target.CashFlowInputId = 4;
                    target.Period = 4;
                    target.TransactionInputId = 1;
                }
                else
                {
                    var original = data.FirstOrDefault(m => m.CashFlowInputId == target.CashFlowInputId);
                    if (original == null)
                        return Task.FromResult(new Result { IsSuccess = false });
                    original = target;
                }

                return Task.FromResult(new Result { IsSuccess = true, Id = target.CashFlowInputId });
            });

            MockCashFlowInputDataAccess = _moqCashFlowInputDataAccess.Object;
        }

        private IEnumerable<CashFlowInput> SetupData()
        {
            var data = new List<CashFlowInput>();
            data.Add(new CashFlowInput { Amount = 100, CashFlowInputId = 1, Period = 1, TransactionInputId = 1 });
            data.Add(new CashFlowInput { Amount = 200, CashFlowInputId = 2, Period = 2, TransactionInputId = 1 });
            data.Add(new CashFlowInput { Amount = 300, CashFlowInputId = 3, Period = 3, TransactionInputId = 1 });
            foreach (var d in data)
                yield return d;
        }
    }
}
