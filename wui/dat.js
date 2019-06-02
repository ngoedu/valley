var DATA = (function() {
	var category = '[{"mid" :"msprog", "name": "初高中编程"},{"mid" :"sweb", "name": "Web服务器端"},{"mid" :"java", "name": "Java基础"},{"mid" :"cweb", "name": "Web前端"},{"mid" :"python", "name": "Python基础"},{"mid" :"mysql", "name": "SQL数据库"}]';
	var courses = '[{"mid" : "cweb", "cid":"sweb-a01-proj1", "name" : "web前端趣味编程a01","target":"初级","duration":"8X12分钟", "content":"HTML,CSS,JS","type":"收费5元","desc":"web客户端编程基础,通过趣味程序带领学生快速上手,深入浅出,快速掌握web前端编程要点"},{"mid" : "python", "cid":"pybasic-a01", "name" : "python趣味编程a01","target":"初级","duration":"8X12分钟", "content":"python","type":"收费5元","desc":"python端编程基础,通过趣味程序带领学生快速上手,深入浅出,快速掌握web前端编程要点"},{"mid" : "sweb", "cid":"sweb-a02-proj2", "name" : "web服务器编程a01","target":"初级","duration":"8X12分钟", "content":"JSP,servlet","type":"免费","desc":"web服务器端编程，指导您开始服务器端的编程体验"}]';
	var topic = '[{"tid" :"msprog", "name": "Python"},{"tid" :"msprog", "name": "Java"},{"tid" :"cweb", "name": "Html"},{"tid" :"cweb", "name": "Css"},{"tid" :"cweb", "name": "Javascript"},{"tid" :"cweb", "name": "jQuery"},{"tid" :"cweb", "name": "H5 Canvas"},{"tid" :"cweb", "name": "Vue"},{"tid" :"sweb", "name": "Servlet"},{"tid" :"sweb", "name": "JSP"},{"tid" :"sweb", "name": "Struts"},{"tid" :"sweb", "name": "Mybatis"},{"tid" :"sweb", "name": "Hibernate"},{"tid" :"sweb", "name": "SpringMVC"},{"tid" :"java", "name": "Java"},{"tid" :"java", "name": "Algorithm"},{"tid" :"java", "name": "Data Structure"},{"tid" :"mysql", "name": "SQL"},{"tid" :"mydql", "name": "Stored Procedure"}]';
	
	return {
		courses: courses,
		category: category,
		topic: topic
	};
})();

