/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/22
 * Time: 23:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Linq;

namespace XTendLibs
{
	/// <summary>
	/// Description of FolderCopy.
	/// </summary>
	public class FolderCopy
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sourceDirName"></param>
		/// <param name="destDirName"></param>
		/// <param name="copySubDirs"></param>
		/// <param name="ignore1"> file name suffix ignore list</param>
		/// <param name="ignore2">folder name ignore list</param>
		public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, string[] ignore1, string[] ignore2)
	    {
	      DirectoryInfo dir = new DirectoryInfo(sourceDirName);
	      DirectoryInfo[] dirs = dir.GetDirectories();
	
	      // If the source directory does not exist, throw an exception.
	        if (!dir.Exists)
	        {
	            throw new DirectoryNotFoundException(
	                "Source directory does not exist or could not be found: "
	                + sourceDirName);
	        }
	
	        // If the destination directory does not exist, create it.
	        if (!Directory.Exists(destDirName))
	        {
	        	Directory.CreateDirectory(destDirName);
	        }
	
	
	        // Get the file contents of the directory to copy.
	        FileInfo[] files = dir.GetFiles();
	
	        foreach (FileInfo file in files)
	        {
	            // Create the path to the new copy of the file.
	            string temppath = Path.Combine(destDirName, file.Name);
	
	            // Copy the file.
	            if (ignore1 != null && ignore1.Any(x=> x == file.Extension))
	            {
	            	//ignore this file
	            } else {
	            	file.CopyTo(temppath, false); 
	            }
	            	
	        }
	
	        // If copySubDirs is true, copy the subdirectories.
	        if (copySubDirs)
	        {
	
	            foreach (DirectoryInfo subdir in dirs)
	            {
	            	if (ignore2 !=null && ignore2.Any(x=> x == subdir.Name))
		            {
		            	//ignore this file
		            } else {
		            	// Create the subdirectory.
		                string temppath = Path.Combine(destDirName, subdir.Name);
		
		                // Copy the subdirectories.
		                DirectoryCopy(subdir.FullName, temppath, copySubDirs, ignore1, ignore2);
		            }
	                
	            }
	        }
	    }
	}
}
