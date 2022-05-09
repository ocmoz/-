<%@ page language="C#" autoeventwireup="true" inherits="Module_FM2E_Report_Output_Supervisor, App_Web_lh0ouo4h" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>报表管理</title>
		<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
		<link href="../themes/StyleSheet.css" rel="stylesheet"
			type="text/css" />
		<script language="javascript" type="text/javascript">
		function jump(raq,station){
		    var url = "http://";
		    url += "<%=quieeIP%>";
		    url += "/quiee/reportJsp/showReport.jsp"
            if (raq!="") url += "?raq=/" + raq;
            if ("<%=station%>"!="") url += "&station=" + "<%=station%>";
            window.open(url);
		}
        </script>
	</head>

	<body>
		<div id="main">
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						督导信息月报
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Dudao_Shoufeiyewu_o.raq','')"
								value="收费业务指标" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Dudao_Jidudabiao_o.raq','')"
								value="季度达标评审、月度自查情况" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Dudao_Xingjiyuebao_o.raq','')"
								value="星级评定" />
						</li>
						
						<br />
						<br />
						
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Dudao_Tiaofeiche_o.raq','')"
								value="逃费车信息" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Dudao_Caozuoshiwu_o.raq','')"
								value="操作失误、文明服务、劳动纪律" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Dudao_shoufeicaozuo_o.raq','')"
								value="收费业务操作失误情况月报" />
						</li>
						<%-- 
						<br />
						<br />
						
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Dudao_Zichayuedu_i.raq','')"
								value="月度自查情况" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"
								onclick="javascript:jump('Dudao_Wenmingfuwu_o.raq','')"
								value="文明服务月报" />
						</li>
						--%>
						
					</ul>
				</div>
			</div>
			</div>
	</body>
</html>
