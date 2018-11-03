var DATA = (function() {
	var courses = '[{"mid" : "cweb", "cid":"cweb-a01-proj1", "name" : "web客户端基础","target":"中级","duration":"18分钟", "content":"HTML,css3,javascript构造页面","type":"免费"},{"mid" : "cweb","cid" : "cweb-a02-proj2", "name" : "web客户端基础","target":"中级", "duration":"18分钟", "content":"HTML,css3,javascript构造页面","type":"免费"},{"mid" : "cweb","cid" : "cweb-A03", "name" : "web客户端基础","target":"中级", "duration":"18分钟", "content":"HTML,css3,javascript构造页面","type":"免费"},{"mid" : "sweb","cid" : "sweb-A01", "name" : "web Server端基础","target":"中级", "duration":"18分钟", "content":"Servlet,JSP,Tomcat","type":"免费"},{"mid" : "java","cid" : "java-A01", "name" : "Java开发基础","target":"中级", "duration":"18分钟", "content":"Java及面向对象编程","type":"免费"},{"mid" : "java","cid" : "java-A02", "name" : "数据结构与算法","target":"中级", "duration":"18分钟", "content":"数据结构与算法基础","type":"免费"}]';
	var category = '[{"mid" :"msprog", "name": "初中编程"},{"mid" :"cweb", "name": "Web Client前端"},{"mid" :"sweb", "name": "Web Server后端"},{"mid" :"java", "name": "Java 开发基础"}, {"mid" :"mysql", "name": "MySQL 数据库基础"}, {"cid" :"other", "name": "其他开发技术"}, {"cid" :"history", "name": "课程历史"}]';
	var topic = '[{"tid" :"msprog", "name": "Java"},{"tid" :"msprog", "name": "Python"},{"tid" :"other", "name": "C#"},{"tid" :"other", "name": "Mina"},{"tid" :"cweb", "name": "Javascript"},{"tid" :"cweb", "name": "css"},{"tid":"cweb","name": "html"},{"tid" :"cweb", "name": "H5 Canvas"},{"tid" :"cweb", "name": "Vue"},{"tid" :"cweb", "name": "jQuery"},{"tid" :"sweb", "name": "Struts"},{"tid" :"sweb", "name": "SpringMVC"},{"tid" :"sweb", "name": "Servlet"},{"tid" :"sweb", "name": "WebWork"},{"tid" :"sweb", "name": "Hibernate"},{"tid" :"sweb", "name": "MyBits"},{"tid" :"sweb", "name": "JSP"},{"tid" :"java", "name": "Java"},{"tid" :"java", "name": "Algorithm"},{"tid" :"java", "name": "Data Structure"},{"tid" :"mysql", "name": "SQL"},{"tid" :"mysql", "name": "Stored Procedure"},{"tid" :"mysql", "name": "MySQL DB"}]';
	
	return {
		courses: courses,
		category: category,
		topic: topic
	};
})();

