/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/23
 * Time: 22:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using DiffMatchPatch;

namespace NGO.Train.Entity
{
	/// <summary>
	/// Description of Course.
	/// </summary>
	public class Course
	{
		
		public const int STATUS_DEFAULT = 0;
		public const int STATUS_REFER = 1;
		public const int STATUS_CODE = 2;
		public const int STATUS_SAVE = 3;
		
		
		
		public Schema Schema {set; get;}
		public List<Tile> Apps {set; get;}
		public List<VLink> Videos {set; get;}
		public List<Refer> Refs {set; get;}
		public List<Revision> Milestons {set; get;}
		
		public Course()
		{
		}

		public Revision GetMileStoneByID(int index)
		{
			if (index <0 || index > Milestons.Count)
				return null;
			
			Revision rev = Milestons[index - 1];
			rev.Project = this.Schema.ProjName;
			return rev;
		}

		public VLink GetVideoByID(int index)
		{
			if (index <0 || index > Videos.Count)
				return null;
			
			return Videos[index - 1];
		}
		
		public Refer GetReferByID(int reference)
		{
			if (reference <0 || reference > Refs.Count)
				return null;
			
			return Refs[reference - 1];
		}

		public Revision GetLatestMileStone()
		{
			return Milestons[Milestons.Count - 1];
		}
	
		/// <summary>
		/// Restore all revision based on the it's patch and source of previous revision code.
		/// </summary>
		public void RestoreRevFiles() {
			if ( this.Milestons.Count <=1) {
				return;
			}
			
			//rebuild baseline revision which on index "0"
			var revBase = this.Milestons[0];
			foreach(var f in revBase.Files) {
				f.Code = f.Content;
			}
			
			//restore src codes for subsequent revisions	
			for (int i=1; i<this.Milestons.Count; i++){
				var rev = this.Milestons[i];
				
				//apply patch for current rev
				foreach(var f in rev.Files) {
					var bf = revBase.Files.Find(x=> x.Name == f.Name);
					if (bf == null)
						f.Code = f.Content;
					else {
						f.Code = RestoreFileCode(bf.Code, f.Content);
						f.Diff = CreateDiff(bf.Code,f.Code);
					}				
				}
				
				revBase = rev;
			}
		}
		
		private my.utils.MyDiff.Item[] CreateDiff(string textA, string textB) {
			return new my.utils.MyDiff().DiffText(textA, textB);
		}
		
		private string RestoreFileCode(string baseSrc, string patchText) {
			if (string.IsNullOrEmpty(baseSrc)) {
				return null;
			}
			
			if (string.IsNullOrEmpty(patchText))
				return baseSrc;

			diff_match_patch patch = new diff_match_patch();
			
			//3. instantiate patch obj from text file
		    var myPatch = patch.patch_fromText(patchText);

			//4. apply patch to get new text    
		    var results = patch.patch_apply(myPatch, baseSrc);
		   
		    string newSrc = (string)results[0];
			
		    return newSrc;
		}
	}
}
