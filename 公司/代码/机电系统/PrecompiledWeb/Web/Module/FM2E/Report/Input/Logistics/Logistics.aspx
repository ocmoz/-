<%@ page language="C#" autoeventwireup="true" inherits="Module_FM2E_Report_Input_Logistics, App_Web_7o-erc5y" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>报表管理</title>
		<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
		<link href="../themes/StyleSheet.css" rel="stylesheet"
			type="text/css" />
		<script language="javascript" charset="gbk" type="text/javascript">
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
		    if (raq != "") {
		        if (raq == "Houqin_carmanager_i.raq") {
		            url += "?raq=/" + "Houqin_carmanager_" + "<%=StaName%>";
		        } else {
		            url += "?raq=/" + raq;
		        } 
		    }
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
						后勤月报
					</div>
				</div>
			</div>
			
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button"  id="b1" runat="server"
								onclick="javascript:jump('Houqin_watereletrioil_i.raq','a')"
								value="水电柴油" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"/>
						</li>
						
						<li>
							<input type="button"  id="b2" runat="server"
								onclick="javascript:jump('Houqin_diningroommanager_i.raq','a')"
								value="食堂管理" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"/>
						</li>
						
						<li>
							<input type="button"  id="b3" runat="server"
								onclick="javascript:jump('Houqin_diningroom_i.raq','a')"
								value="食堂开支" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id="b4" runat="server"
								onclick="javascript:jump('Houqin_diningroomenergy_i.raq','a')"
								value="食堂能耗" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"/>
						</li>
						<li>
							<input type="button"  id="b5" runat="server"
								onclick="javascript:jump('Houqin_carmanager_i.raq','a')"
								value="车辆管理" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"/>
						</li>
						<br />
						<br />
						
						<li>
							<input type="button"  id="b6" runat="server"
								onclick="javascript:jump('Houqin_securitymanager_i.raq','a')"
								value="安全管理" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"/>
						</li>
						
						<li>
							<input type="button"  id="b7" runat="server"
								onclick="javascript:jump('Houqin_staitonbudgetaction_i.raq','a')"
								value="预算管理" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"/>
						</li>
						
						
						
					</ul>
				</div>
			</div>
			</div>
	</body>
</html>
