using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitRuisseau_william;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Windows.Forms;

namespace BitRuisseau_william.Tests
{

    [TestClass()]
    public class Form1Tests
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {

            Directory.CreateDirectory(@"C:\media");


            var filePath1 = @"C:\media\file1.mp3";
            var filePath2 = @"C:\media\file2.png";

            if (!File.Exists(filePath1))
                File.WriteAllBytes(filePath1, new byte[10]);
            if (!File.Exists(filePath2))
                File.WriteAllBytes(filePath2, new byte[10]);
        }
        [TestMethod()]
        public void AddTitle_Click_Test()
        {
            // Arrange
            var mockFileDialogService = new Mock<IFileDialogService>();
            mockFileDialogService.Setup(fd => fd.ShowDialog()).Returns(true);
            mockFileDialogService.Setup(fd => fd.FileNames).Returns(new[] {
            "C:\\media\\file1.mp3",
            "C:\\media\\file2.png"
            });

            var mediatheque = new Mediatheque();
            var mediaPaths = new Dictionary<Media, string>();
            var listView = new ListView();

            var form = new Form1(mockFileDialogService.Object)
            {
                mediatheque = mediatheque,
                mediaPaths = mediaPaths,
                listView_myFiles = listView
            };

            // Act
            form.AddTitle_Click(null, EventArgs.Empty);


            // Assert
            Assert.AreEqual(2, mediatheque.Medias.Count);
            Assert.AreEqual("file1.mp3", mediatheque.Medias[0].FileName);
            Assert.AreEqual("file2.png", mediatheque.Medias[1].FileName);
            Assert.AreEqual(2, listView.Items.Count);
        }
    }
}