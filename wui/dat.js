var DATA = (function() {
	var courses = '[{"mid" : "cweb", "cid":"sweb-a01-proj1", "name" : "web客户端基础","target":"中级","duration":"18分钟", "content":"HTML,css3,javascript构造页面","type":"免费"},{"mid" : "cweb","cid" : "sweb-a02-proj2", "name" : "web客户端基础","target":"中级", "duration":"18分钟", "content":"HTML,css3,javascript构造页面","type":"免费"},{"mid" : "msprog","cid" : "pybasic-a01", "name" : "python基础","target":"初级", "duration":"18分钟", "content":"Python基础语法","type":"免费"},{"mid" : "msprog","cid" : "java-a01", "name" : "Java编程基础","target":"初级", "duration":"18分钟", "content":"Java基础语法","type":"免费"},{"mid" : "java","cid" : "java-b02", "name" : "Java开发基础","target":"中级", "duration":"18分钟", "content":"Java及面向对象编程","type":"免费"},{"mid" : "other","cid" : "SQL-a01", "name" : "数据库基础","target":"中级", "duration":"18分钟", "content":"数据库基础","type":"免费"}]';
	var category = '[{"mid" :"msprog", "name": "初中编程"},{"mid" :"cweb", "name": "Web Client前端"},{"mid" :"sweb", "name": "Web Server后端"},{"mid" :"java", "name": "Java 开发基础"}, {"mid" :"mysql", "name": "MySQL 数据库基础"}, {"cid" :"other", "name": "其他开发技术"}, {"cid" :"history", "name": "课程历史"}]';
	var topic = '[{"tid" :"msprog", "name": "Java"},{"tid" :"msprog", "name": "Python"},{"tid" :"other", "name": "C#"},{"tid" :"other", "name": "Mina"},{"tid" :"cweb", "name": "Javascript"},{"tid" :"cweb", "name": "css"},{"tid":"cweb","name": "html"},{"tid" :"cweb", "name": "H5 Canvas"},{"tid" :"cweb", "name": "Vue"},{"tid" :"cweb", "name": "jQuery"},{"tid" :"sweb", "name": "Struts"},{"tid" :"sweb", "name": "SpringMVC"},{"tid" :"sweb", "name": "Servlet"},{"tid" :"sweb", "name": "WebWork"},{"tid" :"sweb", "name": "Hibernate"},{"tid" :"sweb", "name": "MyBits"},{"tid" :"sweb", "name": "JSP"},{"tid" :"java", "name": "Java"},{"tid" :"java", "name": "Algorithm"},{"tid" :"java", "name": "Data Structure"},{"tid" :"mysql", "name": "SQL"},{"tid" :"mysql", "name": "Stored Procedure"},{"tid" :"mysql", "name": "MySQL DB"}]';
	
	return {
		courses: courses,
		category: category,
		topic: topic
	};
})();

