using DevServ.Web.Api;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Xunit;

namespace DevServ.Web.Tests
{
    public class DevelopersControllersTests
    {
        [Fact]
        public async void List_RepositoryNull_ReturnsInternalServerError()
        {
            var sut = new DevelopersController(null);

            var result = await sut.List() as StatusCodeResult;

            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async void FilteredList_RepositoryNull_ReturnsInternalServerError()
        {

        }
        
        [Fact]
        public async void GetById_RepositoryNull_ReturnsInternalServerError()
        {

        }

        [Fact]
        public async void Post_RepositoryNull_ReturnsInternalServerError()
        {

        }

        [Fact]
        public async void Update_RepositoryNull_ReturnsInternalServerError()
        {

        }

        [Fact]
        public async void Delete_RepositoryNull_ReturnsInternalServerError()
        {

        }

    }
}
