<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="ObservationPortal.Pages.Dashboard"
    MasterPageFile="~/Admin.Master" %>

<asp:Content ID="ContentHead"
    ContentPlaceHolderID="head"
    runat="server">

<style>

.dashboard-header{

display:flex;

justify-content:space-between;

align-items:center;

margin-bottom:30px;


}

.summary-card{

background:white;

border-radius:18px;

padding:22px;

box-shadow:0 8px 25px rgba(0,0,0,.08);

transition:.3s;

height:100%;

position:relative;

overflow:hidden;

}

.summary-card:hover{

transform:translateY(-6px);

}

.summary-card::before{

content:"";

position:absolute;

left:0;

top:0;

width:6px;

height:100%;

}

.blue::before{

background:#0056D2;

}

.green::before{

background:#0F9D58;

}

.orange::before{

background:#F4B400;

}

.red::before{

background:#DB4437;

}

.cyan::before{

background:#00BCD4;

}

.gray::before{

background:#757575;

}

.black::before{

background:#222;

}

.summary-card h6{

color:#666;

font-weight:600;

font-size:15px;

margin-bottom:10px;

}

.summary-card h2{

font-size:42px;

font-weight:700;

margin:0;

color:#222;

}

.chart-card{

background:white;

border-radius:20px;

box-shadow:0 10px 25px rgba(0,0,0,.08);

padding:20px;

height:100%;

}

.chart-title{

font-size:18px;

font-weight:700;

color:#004C97;

margin-bottom:20px;

border-left:5px solid #004C97;

padding-left:12px;

}

.badge{

    padding:8px 14px;

    border-radius:30px;

    font-size:13px;

    font-weight:600;

    letter-spacing:.4px;

}

</style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="dashboard-header mb-4">

    <div>
       
        <h1 class="fw-bold">
    <asp:Label ID="lblGreeting" runat="server"></asp:Label>
</h1>

<p class="text-muted">
    <asp:Label ID="lblDate" runat="server"></asp:Label>
</p>

        <p class="text-muted mb-0">

            Here's today's observation summary.

        </p>

    </div>

   <div class="text-end">

    <div id="liveClock"
         style="
            font-size:24px;
            font-weight:700;
            color:#004C97;">
    </div>

    <div id="liveDate"
         style="
            color:#777;
            margin-top:4px;
            margin-bottom:18px;
            font-size:14px;">
    </div>

    <a href="Observation.aspx"
       class="btn btn-primary">

        <i class="bi bi-plus-circle"></i>

        New Observation

    </a>

</div>

</div>

    <div class="chart-card mb-4">

    <div class="row">

        <div class="col-md-3">

            <label class="form-label fw-semibold">
                Department
            </label>

            <asp:DropDownList
                ID="ddlFilterDepartment"
                runat="server"
                CssClass="form-select">
            </asp:DropDownList>

        </div>

        <div class="col-md-3">

            <label class="form-label fw-semibold">
                Status
            </label>

            <asp:DropDownList
                ID="ddlFilterStatus"
                runat="server"
                CssClass="form-select">

                <asp:ListItem Value="">All</asp:ListItem>
                <asp:ListItem>Open</asp:ListItem>
                <asp:ListItem>WIP</asp:ListItem>
                <asp:ListItem>Closed</asp:ListItem>

            </asp:DropDownList>

        </div>

        <div class="col-md-3">

            <label class="form-label fw-semibold">
                Priority
            </label>

            <asp:DropDownList
                ID="ddlFilterPriority"
                runat="server"
                CssClass="form-select">

                <asp:ListItem Value="">All</asp:ListItem>
                <asp:ListItem>High</asp:ListItem>
                <asp:ListItem>Medium</asp:ListItem>
                <asp:ListItem>Low</asp:ListItem>

            </asp:DropDownList>

        </div>

        <div class="col-md-3 d-flex align-items-end">

            <asp:Button
                ID="btnApplyFilter"
                runat="server"
                Text="Apply Filter"
                CssClass="btn btn-primary w-100"
                OnClick="btnApplyFilter_Click" />

        </div>

    </div>

</div>
    <div class="row g-4">

    <!-- Total -->
    <div class="col-lg-3 col-md-6">
        <div class="summary-card blue">
            <div>
                <h6>Total Observations</h6>
                <h2>
                    <asp:Label ID="lblTotal" runat="server" />
                </h2>
            </div>
        </div>
    </div>

    <!-- Open -->
    <div class="col-lg-3 col-md-6">

    <a href="Reports.aspx?status=Open"
       style="text-decoration:none;color:inherit;">

        <div class="summary-card green">

            <div>

                <h6>Open</h6>

                <h2>
                    <asp:Label ID="lblOpen" runat="server" />
                </h2>

            </div>

        </div>

    </a>

</div>

    <!-- WIP -->
<div class="col-lg-3 col-md-6">

    <a href="Reports.aspx?status=WIP"
       style="text-decoration:none;color:inherit;">

        <div class="summary-card orange">

            <div>

                <h6>WIP</h6>

                <h2>
                    <asp:Label ID="lblWIP" runat="server" />
                </h2>

            </div>

        </div>

    </a>

</div>

    <!-- Closed -->
<div class="col-lg-3 col-md-6">

    <a href="Reports.aspx?status=Closed"
       style="text-decoration:none;color:inherit;">

        <div class="summary-card red">

            <div>

                <h6>Closed</h6>

                <h2>
                    <asp:Label ID="lblClosed" runat="server" />
                </h2>

            </div>

        </div>

    </a>

</div>

    <!-- Total Departments -->
    <div class="col-lg-4 col-md-6">
        <div class="summary-card cyan">
            <div>
                <h6>Departments</h6>
                <h2>
                    <asp:Label ID="lblDepartmentCount" runat="server" />
                </h2>
            </div>
        </div>
    </div>

    <!-- Observation Types -->
    <div class="col-lg-4 col-md-6">
        <div class="summary-card gray">
            <div>
                <h6>Observation Types</h6>
                <h2>
                    <asp:Label ID="lblObservationTypeCount" runat="server" />
                </h2>
            </div>
        </div>
    </div>

    <!-- High Priority -->
<div class="col-lg-4 col-md-6">

    <a href="Reports.aspx?priority=High"
       style="text-decoration:none;color:inherit;">

        <div class="summary-card black">

            <div>

                <h6>High Priority</h6>

                <h2>
                    <asp:Label ID="lblHighPriority" runat="server" />
                </h2>

            </div>

        </div>

    </a>

</div>

</div>


<br />

<div class="row mt-4">

    <!-- Status Chart -->
    <div class="col-lg-6">

        <div class="chart-card">

            <div class="chart-title">                
                Observation Status
            </div>

            <div class="card-body">

                <canvas id="statusChart" height="250"></canvas>

           </div>

        </div>

    </div>

    <!-- Department Chart -->
    <div class="col-lg-6">

        <div class="chart-card">

           <div class="chart-title">
                Department Wise Observations
            </div>

            <div class="card-body">

                <canvas id="departmentChart" height="250"></canvas>

           </div>

        </div>

    </div>

</div>

    <br />

<div class="chart-card">

    <div class="chart-title">
        Recent Observations
    </div>

    <asp:GridView
        ID="gvRecentObservations"
        runat="server"
        CssClass="table table-hover table-bordered"
        AutoGenerateColumns="False"
        GridLines="None">

        <Columns>

            <asp:BoundField
                DataField="SerialNo"
                HeaderText="Serial No" />

            <asp:BoundField
                DataField="DepartmentName"
                HeaderText="Department" />

            <asp:BoundField
                DataField="ObservationTypeName"
                HeaderText="Observation Type" />

            <asp:TemplateField HeaderText="Priority">

    <ItemTemplate>

        <span class='<%#
            Eval("Priority").ToString()=="High" ? "badge bg-danger" :
            Eval("Priority").ToString()=="Medium" ? "badge bg-warning text-dark" :
            "badge bg-success"
        %>'>

            <%# Eval("Priority") %>

        </span>

    </ItemTemplate>

</asp:TemplateField>

            <asp:TemplateField HeaderText="Status">

    <ItemTemplate>

        <span class='<%#
            Eval("Status").ToString()=="Open" ? "badge bg-success" :
            Eval("Status").ToString()=="WIP" ? "badge bg-warning text-dark" :
            "badge bg-danger"
        %>'>

            <%# Eval("Status") %>

        </span>

    </ItemTemplate>

</asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>
    <asp:Label ID="lblOpenChart"
    runat="server"
    Style="display:none;"></asp:Label>

<asp:Label ID="lblWIPChart"
    runat="server"
    Style="display:none;"></asp:Label>

<asp:Label ID="lblClosedChart"
    runat="server"
    Style="display:none;"></asp:Label>

    <asp:HiddenField ID="hfDepartmentLabels" runat="server" />
<asp:HiddenField ID="hfDepartmentCounts" runat="server" />
    <script>

        window.onload = function () {

            var open = parseInt(document.getElementById('<%= lblOpenChart.ClientID %>').textContent);
            var wip = parseInt(document.getElementById('<%= lblWIPChart.ClientID %>').textContent);
            var closed = parseInt(document.getElementById('<%= lblClosedChart.ClientID %>').textContent);

            new Chart(document.getElementById("statusChart"), {

                type: 'pie',

                data: {
                    labels: ['Open', 'WIP', 'Closed'],
                    datasets: [{
                        data: [open, wip, closed],
                        backgroundColor: [
                            '#28a745',
                            '#ffc107',
                            '#dc3545'
                        ]
                    }]
                },

                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'bottom'
                        }
                    }
                }

            });



            var labels = document.getElementById('<%= hfDepartmentLabels.ClientID %>').value.split(',');
            var values = document.getElementById('<%= hfDepartmentCounts.ClientID %>').value.split(',');

            new Chart(document.getElementById("departmentChart"), {

                type: 'bar',

                data: {
                    labels: labels,
                    datasets: [{
                        label: "Observations",
                        data: values,
                        backgroundColor: '#198754'
                    }]
                },

                options: {

                    responsive: true,

                    plugins: {
                        legend: {
                            display: false
                        }
                    },

                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }

                }

            });

        };

    </script>

    <script>

        function updateClock() {

            const now = new Date();

            const time = now.toLocaleTimeString([], {

                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
                hour12: true

            });

            const date = now.toLocaleDateString('en-GB', {

                weekday: 'long',
                day: '2-digit',
                month: 'long',
                year: 'numeric'

            });

            document.getElementById("liveClock").innerHTML =
                "🕒 " + time;

            document.getElementById("liveDate").innerHTML =
                "📅 " + date;

        }

        updateClock();

        setInterval(updateClock, 1000);

    </script>
</asp:Content>