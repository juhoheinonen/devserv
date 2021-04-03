using DevServ.Web.Api;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace DevServ.Web.Tests
{
    public class DevelopersControllersTests
    {
        [Fact]
        public async void List_RepositoryNull_ReturnsInternalServerError()
        {
            var sut = new DevelopersController(null, new FakeLogger());

            var result = await sut.List() as StatusCodeResult;

            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async void FilteredList_RepositoryNull_ReturnsInternalServerError()
        {
            var sut = new DevelopersController(null, new FakeLogger());

            var skills = new List<string> { "javascript" };

            var result = await sut.FilteredList(skills) as StatusCodeResult;

            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
        
        [Fact]
        public async void GetById_RepositoryNull_ReturnsInternalServerError()
        {
            var sut = new DevelopersController(null, new FakeLogger());

            var result = await sut.GetById(0) as StatusCodeResult;

            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async void Post_RepositoryNull_ReturnsInternalServerError()
        {
            var sut = new DevelopersController(null, new FakeLogger());

            var result = await sut.List() as StatusCodeResult;

            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async void Update_RepositoryNull_ReturnsInternalServerError()
        {
            var sut = new DevelopersController(null, new FakeLogger());

            var result = await sut.Update(null) as StatusCodeResult;

            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async void Delete_RepositoryNull_ReturnsInternalServerError()
        {
            var sut = new DevelopersController(null, new FakeLogger());

            var result = await sut.Delete(0) as StatusCodeResult;

            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        public class FakeLogger : ILogger
        {
            public void Write(LogEvent logEvent)
            {
                Console.WriteLine(logEvent.ToString());
            }
        }
    }
}
