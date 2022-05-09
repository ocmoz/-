<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Supervisor.aspx.cs" Inherits="Module_FM2E_Report_Input_Supervisor" %>

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
		    var ip1="<%=quieeIP1%>";
		    var ip2="<%=quieeIP2%>";
		    var tempurl =document.location.href;
		    if (tempurl.indexOf(ip2)>0) {
		        url += "<%=quieeIP1%>";
		    }
		    else{
		    	url += "<%=quieeIP%>";	    
		    }
		    url += "/quiee/reportJsp/showReport.jsp";
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
							<input type="button"  id="b1" runat="server"
								onclick="javascript:jump('Dudao_Shoufeiyewu_i.raq','')"
								value="收费业务指标" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id="b2" runat="server"
								onclick="javascript:jump('Dudao_Jidudabiao_i.raq','')"
								value="季度达标评审情况" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id="b3" runat="server"
								onclick="javascript:jump('Dudao_Zichayuedu_i.raq','')"
								value="月度自查情况" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"/>
						</li>
						<br/>
						<br/>
						<li>
							<input type="button"  id="b4" runat="server"
								onclick="javascript:jump('DuDao_Xingjiyuebao_i.raq','')"
								value="星级评定" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"/>
						</li>
											
						<li>
							<input type="button"  id="b5" runat="server"
								onclick="javascript:jump('Dudao_Tiaofeiche_i.raq','')"
								value="逃费车信息" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id="b6" runat="server"
								onclick="javascript:jump('Dudao_Caozuoshiwu_i.raq','')"
								value="操作失误、文明服务、劳动纪律" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"/>
						</li>
						<br/>
						<br/>
						<li>
							<input type="button"  id="b7" runat="server"
								onclick="javascript:jump('Dudao_Shoufeicaozuo_i.raq','')"
								value="收费业务操作失误情况月报" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"/>
						</li>
						<%-- 
						<li>
							<input type="button"  
								onclick="javascript:jump('Dudao_Zichayuebao_i.raq','')"
								value="月度自查情况" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  
								onclick="javascript:jump('Dudao_Wenmingfuwu_i.raq','')"
								value="文明服务月报" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 200px;cursor: hand;"/>
						</li>
						--%>
						
					</ul>
				</div>
			</div>
			</div>
	</body>
</html>
