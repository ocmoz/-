<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Electromechanics.aspx.cs" Inherits="Module_FM2E_Report_Input_Electromechanics" %>

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
						机电弱电信息月报
					</div>
				</div>
			</div>
			
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" id="b1" runat="server"
								onclick="javascript:jump('Jidian_Ruodian_SystemOperationMonthly_i.raq','a')"
								value="系统运行情况表"  style="font-family: 微软雅黑;height: 29px; text-align: center; width: 170px;cursor: hand;"/>
						</li>
						<li>
							<input type="button" id="b2" runat="server"
								onclick="javascript:jump('Jidian_Ruodian_SystemOuterRepairMonthly_i.raq','a')"
								value="代维情况月报" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 170px;cursor: hand;" />
						</li>
						<li>
							<input type="button" id="b3" runat="server"
								onclick="javascript:jump('Jidian_Ruodian__SystemBasicData_i.raq','a')"
								value="基础数据" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 170px;cursor: hand;"/>
						</li>
					</ul>
				</div>
			</div>
			<%-- 
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						机电信息月报
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" id="b4" runat="server"
								onclick="javascript:jump('Jidian_Jidian_MechElecInfoMonthly_i.raq','a')"
								value="收费站机电信息月报" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 170px;cursor: hand;"/>
						</li>
						<li>
							<input type="button" id="b5" runat="server"
								onclick="javascript:jump('Jidian_Jidian_MechElecInfoMonthlySummary_i.raq','a')"
								value="机电信息月报汇总" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 170px;cursor: hand;"/>
						</li>
						
						
					</ul>
				</div>
				--%>
			</div>
			</div>
	</body>
</html>
