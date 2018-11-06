/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/8/24
 * Time: 23:42
 * 
 * 
 */
using System;
using System.Collections.Generic;
using DiffMatchPatch;
using NGO.Train.Entity;
using System.IO;

namespace XCodeRec
{
	/// <summary>
	/// Description of RevBuilder.
	/// </summary>
	public class RevBuilder
	{
		public RevBuilder()
		{
		}
		
		public static Revision FirstRevision(string firstDir)
	    {
			var txtFiles = Directory.EnumerateFiles(firstDir, "*.*", SearchOption.AllDirectories);

			var rev = new Revision();
			rev.ID = 1;
           	rev.Files = new List<NGO.Train.Entity.File>();
            	
            foreach (string currentFile in txtFiles)
            {
            	var file = new NGO.Train.Entity.File();
            	file.Path = currentFile.Substring(firstDir.Length + 1);
            	int lidx = file.Path.LastIndexOf('\\');
            	file.Name =  lidx == -1 ? file.Path : file.Path.Substring(lidx+1);
            	file.Content = System.IO.File.ReadAllText(currentFile);
            	rev.Files.Add(file);
            }
            
            return rev;
            
		}
		
		public static Revision NextRevision(int rid, string oldDir, string newDir)
	    {
			var txtFiles = Directory.EnumerateFiles(newDir, "*.*", SearchOption.AllDirectories);

			
			var rev = new Revision();
			rev.ID = rid;
           	rev.Files = new List<NGO.Train.Entity.File>();
            
           	
            foreach (string currentFile in txtFiles)
            {
                string fileName = currentFile.Substring(newDir.Length + 1);
                
                if (System.IO.File.Exists(oldDir+@"\"+fileName)) {
                	 List<Patch> patches;
				    diff_match_patch patch = new diff_match_patch();
				    
				    string oldText = System.IO.File.ReadAllText(oldDir+@"\"+fileName);
				    string newText = System.IO.File.ReadAllText(currentFile);
				    //1. make patch obj
				    patches = patch.patch_make(oldText, newText);
				    
				    //2. convert patch obj to text and persiste in file
				    var patchText = patch.patch_toText(patches);
				    
				    var file = new NGO.Train.Entity.File();
	            	file.Path =  fileName;
	            	int lidx = file.Path.LastIndexOf('\\');
            		file.Name =  lidx == -1 ? file.Path : file.Path.Substring(lidx+1);
            	
	            	file.Content = patchText;
	            	rev.Files.Add(file);
                }
                
            }
            
            return rev;
		}
	}
}
