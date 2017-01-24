using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Caching;
using Library.Tests.Helpers;
using Library.Tests.Mocks;

namespace Library.Tests.Fixtures
{
    [TestFixture]
    public class PageCacheCoordinatorFixture
    {
        [Test]
        public void GetCachedPage_PageNotExists_ReturnsNull()
        {
            var cacheCoordinator = GetPageCacheCoordinator();
            var page = cacheCoordinator.TryGetCachedPage("NonExistentPage");
            Assert.That(page, Is.Null);
        }

        public static PageCacheCoordinator GetPageCacheCoordinator()
        {
            StateManager.ResetAll();
            return new PageCacheCoordinator();
        }
    }
}
