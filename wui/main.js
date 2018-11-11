var MAINUI = (function() {
	
	function init() {
		$('.modal').hide();
	}
	
	function buildRollings(box)
	{
		var topic = JSON.parse(DATA.topic);	
		for(var i = 0; i < topic.length; i++) {
			var obj = topic[i];
			var ahref = $("<a/>",{href:"#"+obj.tid,text:obj.name});
			$('#'+box).append(ahref);
		}
	}
	
	function buildCategory(box) {
		var ul = $("<ul/>");
		$('#'+box).append(ul);	
		//build entries
		var category = JSON.parse(DATA.category);
		for(var i = 0; i < category.length; i++) {
			var obj = category[i];
			var li = $("<li/>").addClass("course-item");
			$(ul).append(li);
			var ahref = $("<a/>",{id:"category"+i,href:"javascript:void(0);",text:obj.name});
			$(li).append(ahref);
			var mid = obj.mid;
			$(ahref).on( "click", { name: mid, oid: "category"+i}, showup );
		}
	}
	
	function showup( event ) {
	  ROLL.markChild(event.data.name);
	  //alert("event.data.oid="+event.data.oid);
	  $(".course-item").removeClass("course-selected");
	  $("#"+event.data.oid).parent().addClass("course-selected");
	  listCourses(event.data.name,'course_desc');
	}
	
	var currMid;
	var currBox;
	
	function listCourses(mid, box)
	{
		//cache mid,box
		currMid = mid;
		currBox = box;
		
		//remove all childs course info
		$('#'+box).children().remove();
		
		var course = JSON.parse(DATA.courses);	
		for(var i = 0; i < course.length; i++) {
			var obj = course[i];
			if (obj.mid !==mid)
				continue;
			var div = $("<div/>");
			var span = $("<span/>").text("课程名称："+obj.cid +","+ obj.name);
			$(div).append(span);
			
			var ul = $("<ul/>");
			var li = $("<li/>").addClass("course-info").text("适用："+obj.target);
			$(ul).append(li);
			var li = $("<li/>").addClass("course-info").text("时长："+obj.duration);
			$(ul).append(li);
			li = $("<li/>").addClass("course-info").text("内容："+obj.content);
			$(ul).append(li);
			li = $("<li/>").addClass("course-info").text("类型："+obj.type);
			$(ul).append(li);
			$(div).append(ul);
			
			
			hreftext = "详情";
			var ahref = $("<a/>",{id:"id"+i,name:"btnDown",href:"javascript:void(0);",text:hreftext});
			$(div).append(ahref);
			$(ahref).on( "click", { name: obj.cid}, showPreview );
				
			/*
			var hreftext = "";
			var downloadedList = CourseJScallback.getDownloadedList();
			var downloaded = downloadedList.split(',');
			
			if (downloaded.indexOf(obj.cid) > -1) {
				hreftext = "开始";
				var ahref = $("<a/>",{id:"id"+i,name:"btnStart",href:"javascript:void(0);",text:hreftext});
				ahref.css("background-color","green");
				$(div).append(ahref);
				$(ahref).on( "click", { name: obj.cid}, startCourse );
			} else {
				hreftext = "下载";
				var ahref = $("<a/>",{id:"id"+i,name:"btnDown",href:"javascript:void(0);",text:hreftext});
				$(div).append(ahref);
				$(ahref).on( "click", { name: obj.cid}, download );
			}
			*/
			
			
			$('#'+box).append(div);
		}		
	}
	
	function download(event) {
		modalDialogShow();
		CourseJScallback.startDownload(event.data.name);
	}
	
	function startCourse(event) {
		var cid = event.data.name;
		CourseJScallback.startPlayCourse(cid);
	}
	
	function downloadStatusChanged(value) {
		$("#progressBar").width(value / 100 * 1226);
	}
	
	function downloadCompleted() {
		modalDialogHide();
		//location.reload(true);
		listCourses(currMid, currBox);
	}
		
	function showPreview(event) {
		var cid = event.data.name;
		CourseJScallback.showCoursePreview(cid);
	}
	
	function modalDialogShow() {
		$('.modal').show();
	}
	
	function modalDialogHide() {
		$('.modal').hide();
	}
	
	return {
		init: init,
		downloadCompleted:downloadCompleted,
		modalDialog : modalDialogShow,
		modalDialog : modalDialogHide,
		listCourses: listCourses,
		buildRollings: buildRollings,
		buildCategory: buildCategory
	};
})();

/*main entry*/
MAINUI.init();
MAINUI.buildCategory('categoryCanvas');
MAINUI.buildRollings('rollingCanvas');
ROLL.load('rollingCanvas');




