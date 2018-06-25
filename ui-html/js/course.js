var COURSES = (function() {
	var data = '[{"cid" : "cweb-A01", "name" : "web客户端基础","target":"中级","duration":"18分钟", "content":"HTML,css3,javascript构造页面","type":"免费"},{"cid" : "cweb-A02", "name" : "web客户端基础","target":"中级", "duration":"18分钟", "content":"HTML,css3,javascript构造页面","type":"免费"},{"cid" : "cweb-A03", "name" : "web客户端基础","target":"中级", "duration":"18分钟", "content":"HTML,css3,javascript构造页面","type":"免费"}]';
	
	function listCourses(box)
	{
		var userdata = JSON.parse(data);	
		for(var i = 0; i < userdata.length; i++) {
			var obj = userdata[i];
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
		listCourses: listCourses
	};
})();

