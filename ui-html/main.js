var MAINUI = (function() {
	
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
			var cid = obj.cid;
			$(ahref).on( "click", { name: cid}, showup );
		}
	}
	
	function showup( event ) {
	  ROLL.markChild(event.data.name);
	}
	
	function listCourses(box)
	{
		var course = JSON.parse(DATA.courses);	
		for(var i = 0; i < course.length; i++) {
			var obj = course[i];
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
			
			var ahref = $("<a/>",{id:"id"+i,name:"btnPrev",href:"javascript:void(0);",text:"预览"});
			$(div).append(ahref);
			$(ahref).click(function(){
				preview(1, 'preview_box');
			});
			
			$('#'+box).append(div);
		}		
	}
	
	function preview(pid, box) {
		$('#'+box).children().remove();;
		var iframe = $('<iframe>', {src: 'file:///D:/neverstop/tutorial/webClient/test2.html', frameborder: 0,  scrolling: 'no' });
		$(iframe).height($('#preview_box').height());
		$(iframe).width($('#preview_box').width());
		$('#'+box).append(iframe);
		//<iframe width="100%" height="100%" src="file:///D:/neverstop/tutorial/webClient/test2.html" frameborder="0" allowfullscreen></iframe>
	}
	
	return {
		listCourses: listCourses,
		buildRollings: buildRollings,
		buildCategory: buildCategory
	};
})();

MAINUI.buildCategory('categoryCanvas');
MAINUI.buildRollings('rollingCanvas');
ROLL.load('rollingCanvas');
MAINUI.listCourses('course_desc');



