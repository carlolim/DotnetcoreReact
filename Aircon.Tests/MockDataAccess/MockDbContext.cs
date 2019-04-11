using Aircon.DataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Tests.MockDataAccess
{
    public class MockDbContext
    {
        public readonly IDbContext MockDbContextDataAccess;
        private readonly Mock<IDbContext> _moqDbContextDataAccess = new Mock<IDbContext>();
        public MockDbContext()
        {
            var mockCashFlowInput = new MockCashFlowInput();
            _moqDbContextDataAccess.Setup(m => m.CashFlowInputDataAccess).Returns(mockCashFlowInput.MockCashFlowInputDataAccess);
            MockDbContextDataAccess = _moqDbContextDataAccess.Object;
        }
    }
}
