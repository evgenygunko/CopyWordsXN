﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CopyWords.Parsers.Tests
{
    [TestClass]
    public class DDOPageParserTests
    {
        private static string _path;

        [ClassInitialize]
        public static void ClassInitialze(TestContext context)
        {
            _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        #region LoadStream tests

        [TestMethod]
        public void LoadHtml_DoesntThowExpception_ForValidString()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("SimplePage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadHtml_ThowsExpception_ForNullString()
        {
            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(null);
        }

        #endregion

        #region ParseWord tests

        [TestMethod]
        public void ParseWord_ReturnsUnderholdning_ForUnderholdningPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("UnderholdningPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string word = parser.ParseWord();
            Assert.AreEqual("underholdning", word);
        }

        [TestMethod]
        public void ParseWord_ReturnsGrillspydPage_ForUGrillspydPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("GrillspydPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string word = parser.ParseWord();
            Assert.AreEqual("grillspyd", word);
        }

        [TestMethod]
        public void ParseWord_ReturnsStødtand_ForUnderholdningPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("StødtandPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string word = parser.ParseWord();
            Assert.AreEqual("stødtand", word);
        }

        #endregion

        #region ParseEndings tests

        [TestMethod]
        public void ParseEndings_ReturnsEn_ForUnderholdningPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("UnderholdningPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string endings = parser.ParseEndings();
            Assert.AreEqual("-en", endings);
        }

        [TestMethod]
        public void ParseEndings_ReturnsEndings_ForStødtandPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("StødtandPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string endings = parser.ParseEndings();
            Assert.AreEqual("-en, ..tænder, ..tænderne", endings);
        }

        #endregion

        #region ParsePronunciation tests

        [TestMethod]
        public void ParsePronunciation_ReturnsUnderholdning_ForUnderholdningPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("UnderholdningPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string pronunciation = parser.ParsePronunciation();
            Assert.AreEqual("[ˈɔnʌˌhʌlˀneŋ]", pronunciation);
        }

        [TestMethod]
        public void ParsePronunciation_ReturnsKigge_ForKiggePage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("KiggePage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string pronunciation = parser.ParsePronunciation();
            Assert.AreEqual("[ˈkigə]", pronunciation);
        }

        [TestMethod]
        public void ParsePronunciation_ReturnsEmptyString_ForGrillspydPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("GrillspydPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string pronunciation = parser.ParsePronunciation();
            Assert.AreEqual(string.Empty, pronunciation);
        }

        #endregion

        #region ParseSound tests

        [TestMethod]
        public void ParseSound_ReturnsSoundFile_ForUnderholdningPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("UnderholdningPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string pronunciation = parser.ParseSound();
            Assert.AreEqual("http://static.ordnet.dk/mp3/12004/12004770_1.mp3", pronunciation);
        }

        [TestMethod]
        public void ParseSound_ReturnsEmptyString_ForKiggePage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("KiggePage.html"));

            // Kigge page doesn't have a sound file
            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string sound = parser.ParseSound();
            Assert.AreEqual(string.Empty, sound);
        }

        [TestMethod]
        public void ParseSound_ReturnsSoundFile_ForDannebrogPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("DannebrogPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string sound = parser.ParseSound();
            Assert.AreEqual("http://static.ordnet.dk/mp3/11008/11008357_1.mp3", sound);
        }

        #endregion

        #region ParseDefinitions tests

        [TestMethod]
        public void ParseDefinitions_ReturnsDefinition_ForUnderholdningPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("UnderholdningPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string definitions = parser.ParseDefinitions();

            const string Expected = "noget der morer, glæder eller adspreder nogen, fx optræden, et lettere og ikke særlig krævende åndsprodukt eller en fornøjelig beskæftigelse";
            Assert.AreEqual(Expected, definitions);
        }

        [TestMethod]
        public void ParseDefinitions_ReturnsConcatenatedDefinitions_ForKiggePage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("KiggePage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string definitions = parser.ParseDefinitions();

            string expected = "1. rette blikket i en bestemt retning" + Environment.NewLine +
                "2. undersøge nærmere; sætte sig ind i" + Environment.NewLine +
                "3. prøve at finde" + Environment.NewLine +
                "4. skrive af efter nogen; kopiere noget" + Environment.NewLine +
                "5. se på; betragte";
            Assert.AreEqual(expected, definitions);
        }

        [TestMethod]
        public void ParseDefinitions_ReturnsEmptyString_ForGrillspydPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("GrillspydPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string definitions = parser.ParseDefinitions();

            Assert.AreEqual(string.Empty, definitions);
        }

        #endregion

        #region ParseExamples tests

        [TestMethod]
        public void ParseExamples_ReturnsExample_ForDannebrogPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("DannebrogPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            string expected = "1. [slidt] er det lille dannebrog, der vajende fra sin hvide flagstang pryder pudens forside.";

            List<string> examples = parser.ParseExamples();

            Assert.AreEqual(1, examples.Count);
            Assert.AreEqual(expected, examples[0]);
        }

        [TestMethod]
        public void ParseExamples_ReturnsConcatenatedExamples_ForUnderholdningPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("UnderholdningPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            List<string> examples = parser.ParseExamples();

            List<string> expected = new List<string>();
            expected.Add("1. 8000 medarbejdere skal til fest med god mad og underholdning af bl.a. Hans Otto Bisgaard.");
            expected.Add("2. vi havde jo ikke radio, TV eller video, så underholdningen bestod mest af kortspil i familien.");

            List<string> differences = expected.Except(examples).ToList();
            Assert.AreEqual(0, differences.Count, "Threre are differences between expected and actual lists with examples.");
        }

        [TestMethod]
        public void ParseExamples_ReturnsConcatenatedExamples_ForKiggePage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("KiggePage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            List<string> examples = parser.ParseExamples();

            List<string> expected = new List<string>();
            expected.Add("1. Børnene kiggede spørgende på hinanden.");
            expected.Add("2. kig lige en gang!");
            expected.Add("3. Han kiggede sig rundt, som om han ledte efter noget.");
            expected.Add("4. hun har kigget på de psykiske eftervirkninger hos voldtagne piger og kvinder.");
            expected.Add("5. Vi kigger efter en bil i det prislag, og Carinaen opfylder de fleste af de krav, vi stiller.");
            expected.Add("6. Berg er ikke altid lige smart, bl.a. ikke når hun afleverer blækregning for sent OG vedlægger den opgave, hun har kigget efter.");
            expected.Add("7. Har du lyst til at gå ud og kigge stjerner, Oskar? Det er sådan et smukt vejr.");

            List<string> differences = expected.Except(examples).ToList();
            Assert.AreEqual(0, differences.Count, "Threre are differences between expected and actual lists with examples.");
        }

        [TestMethod]
        public void ParseExamples_ReturnsEmptyList_ForGrillspydPage()
        {
            string content = Helpers.GetSimpleHTMLPage(GetTestFilePath("GrillspydPage.html"));

            DDOPageParser parser = new DDOPageParser();
            parser.LoadHtml(content);

            List<string> examples = parser.ParseExamples();

            Assert.AreEqual(0, examples.Count);
        }

        #endregion

        private string GetTestFilePath(string fileName)
        {
            return Path.Combine(_path, "TestPages", "ddo", fileName);
        }
    }
}
