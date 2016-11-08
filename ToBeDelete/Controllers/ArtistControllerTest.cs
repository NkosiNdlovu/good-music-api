using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoodMusic;
using GoodMusic.Controllers;

namespace GoodMusic.Tests.Controllers
{
    [TestClass]
    public class ArtistControllerTest
    {
       
        [TestMethod]
        public void Search()
        {
            // arrange
            ArtistController controller = new ArtistController();

            // act
            controller.Search("joh",1, 10);

            //assert
        }
    }
}
