/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/17
 * 时间: 12:49
 * 
 * 
 */
using System;
using NUnit.Framework;
using Snow.X.Algorithm;

namespace Snow.X.UnitTest
{
	[TestFixture]
	public class TestTrieDict
	{
		[Test]
		public void TestMethod_1()
		{
			TrieDict dict = new TrieDict();
			dict.Insert("<a>", 0);
			dict.Insert("<p>", 1);
			
			Assert.AreEqual(0,dict.Scan("<a>"));
			Assert.AreEqual(1,dict.Scan("<p>"));
			Assert.AreEqual(-1,dict.Scan("<a->"));
			
		}
		
		[Test]
		public void TestMethod_2()
		{
			TrieDict dict = new TrieDict();
			dict.Insert("<a ", 0);
			dict.Insert("<p>", 1);
			
			Assert.AreEqual(-1,dict.Scan("<a>"));
			Assert.AreEqual(1,dict.Scan("<p>"));
			Assert.AreEqual(0,dict.Scan("<a somekey>"));
			
		}
	}
}
