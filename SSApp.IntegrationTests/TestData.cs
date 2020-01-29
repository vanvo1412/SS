using SSApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSApp.IntegrationTests
{
    public class TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new Alteration[] {
                new Alteration()
                {
                    Id = 3,
                    LeftLength = 1,
                    RightLength = 1,
                    Status = StatusEnum.Created,
                    Type = AlterationTypeEnum.Sleeve
                }
            };
            yield return new Alteration[] {
                new Alteration()
                {
                    Id = 4,
                    LeftLength = 1,
                    RightLength = 1,
                    Status = StatusEnum.Created,
                    Type = AlterationTypeEnum.Trouser
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
