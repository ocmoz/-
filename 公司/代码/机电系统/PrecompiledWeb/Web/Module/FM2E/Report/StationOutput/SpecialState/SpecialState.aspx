<%@ page language="C#" autoeventwireup="true" inherits="Module_FM2E_Report_Output_SpecialState, App_Web_yjqjrhnk" %>
<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server" id ="head1">
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

	<body runat="server">
		
		<div id="main">

			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						特殊运营状态信息月报
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
				<ul>
					<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('Teshu_yunying_o.raq','')"
								value="特殊运营状态信息统计表" align="middle" />
						</li>
					
						
					</ul>
				</div>
			</div>
		</div>

	</body>
</html>
