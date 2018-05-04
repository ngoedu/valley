/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/5/3
 * 时间: 20:56
 * 
 * 
 */
using System;
using System.Collections.Generic;
using NUnit.Framework;
using DiffMatchPatch;

namespace XDiffPatch.UnitTest
{
	[TestFixture]
	public class TestPatch
	{
		[Test]
		public void TestMethod_1()
		{

		    List<Patch> patches;
		    diff_match_patch patch = new diff_match_patch();
		    
		    string old = "The quick brown fox jumps over the lazy 是dog.";
		    string newText = "That quick brown fox jumpe文d over a lazy 是dog.";
		    //1. make patch obj
		    patches = patch.patch_make(old, newText);
		    
		    //2. convert patch obj to text and persiste in file
		    var patchText = patch.patch_toText(patches);
		    
		    //3. instantiate patch obj from text file
		    var myPatch = patch.patch_fromText(patchText);
		
			//4. apply patch to get new text    
		    var results = patch.patch_apply(patches, old);
		    bool[] boolArray = (bool[])results[1];
		    //string resultStr = results[0] + "\t" + boolArray[0] + "\t" + boolArray[1];
		    string resultStr = (string)results[0];
		    //5. do url decode
		    //resultStr = System.Web.HttpUtility.UrlDecode(resultStr, System.Text.Encoding.UTF8);
		    Assert.AreEqual(newText, resultStr);
		}
		
		[Test]
		public void TestMethod_2()
		{

		    List<Patch> patches;
		    diff_match_patch dmp = new diff_match_patch();
		    
		    string old = "<html>\r\n</html>";
		    string newText = "<html>\r\n<head>标题</head></html>";
		    //1. make patch obj
		    patches = dmp.patch_make(old, newText);
		    
		    //2. convert patch obj to text and persiste in file
		    var patchText = dmp.patch_toText(patches);
		    
		    //3. instantiate patch obj from text file
		    var myPatch = dmp.patch_fromText(patchText);
		
			//4. apply patch to get new text    
		    var results = dmp.patch_apply(patches, old);
		    //bool[] boolArray = (bool[])results[1];
		    string resultStr = (string)results[0];
		    //5. do url decode
		    // SEEMS URLENCODING IS NOT NECESSARY
		   	//resultStr = System.Web.HttpUtility.UrlDecode(resultStr, System.Text.Encoding.UTF8);
		
		    Assert.AreEqual(newText, resultStr);
		}
	}
}
