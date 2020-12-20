<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CSRandom._Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("input[name$='amountTxt']").attr('value', '').val("").focus();
            $("input[name$='Side']").change(function () {
                $("input[name$='amountTxt']").val("").focus();
            });


            $("input[id$='tRb']").on('change click', function () {
                console.log('change')
                var amountTxt = $("input[name$='amountTxt'");
                var amount = 800;
                if ($("#MainContent_radioCasual").prop("checked")) {
                    amount = 1000;
                }
                //if ($("#MainContent_helmetChk").prop("checked"))
                //    amount -= 350;
                //if ($("#MainContent_armorChk").prop("checked"))
                //    amount -= 650;
                amountTxt.val(amount);
                $("input[name$='getWeaponsBtn']").click();
            });

            $(document).keydown(function (e) {
                if (e.keyCode == 37)
                    $("input[id='MainContent_ctRb']").click();
                else if (e.keyCode == 39)
                    $("input[id='MainContent_tRb']").click();
            });

            $("input[name$='amountTxt']").keyup(function (e) {
                if (e.keyCode == 84) {
                    $("#topupCheck").prop("checked", true);
                    $("input[name$='amountTxt']").val("").focus();
                    return;
                }
                var val = $("input[name$='amountTxt']").val();
                if (!((e.keyCode > 47 && e.keyCode < 58) || (e.keyCode > 95 && e.keyCode < 106)))
                    return;
                var numbers = val.match(/[+-]?\d+(?:\.\d+)?/g);
                if (numbers.toString().length == 2) {
                    numbers = numbers * 100;
                    if (val.length > 2)
                        numbers += 50;
                    if ($("#MainContent_radioCompetitive").prop("checked")) {
                        numbers = deductEquipment(numbers);
                    }
                    $("input[name$='amountTxt']").val(numbers);
                    console.log("numbers", numbers);
                    $("input[name$='getWeaponsBtn']").click();
                }
            });
            function deductEquipment(amount) {
                console.log("amountstart", amount);
                if ($("#MainContent_helmetChk").prop("checked"))
                    amount -= 350;
                if ($("#MainContent_armorChk").prop("checked"))
                    amount -= 650;
                console.log("amountend", amount);
                return amount;
            }
        });
    </script>
    <div class="row">
        <div class="col-md-4">

            <div class="line">
                Side: 
        <br />
                <asp:RadioButton ID="ctRb" GroupName="Side" runat="server" Checked="True" Value="CounterTerrorist" Text="Counter-Terrorist" />
                <br />
                <asp:RadioButton ID="tRb" GroupName="Side" runat="server" Value="Terrorist" Text="Terrorist" />
            </div>
        </div>
        <div class="col-md-4">

            <div class="line">
        <br />
                <asp:CheckBox ClientIDMode="Static" ID="topupCheck" runat="server" Text="Topup" Checked="False" />
                <br />
            </div>
            <div class="line">
        <br />
                <asp:CheckBox ClientIDMode="Static" ID="CheckBox1" runat="server" Text="Topup" Checked="False" />
                <br />
            </div>
            <div class="line">
        <br />
                <asp:CheckBox ClientIDMode="Static" ID="CheckBox2" runat="server" Text="Topup" Checked="False" />
                <br />
            </div>

        </div>
                <div class="col-md-4">

            <div class="line">
        <br />
                <asp:DropdownList ClientIDMode="Static" ID="weaponsDDL" runat="server" />
                <br />
            </div>
        </div>

        <div class="col-md-4">
            <div class="line">
                Game type: 
        <br />
                <asp:RadioButton ID="radioCasual" GroupName="Type" runat="server" Checked="True" Value="casual" AutoPostBack="true" Text="Casual" OnCheckedChanged="gameTypeCheckedChanged" />
                <br />
                <asp:RadioButton ID="radioCompetitive" AutoPostBack="true" GroupName="Type" runat="server" Value="competitive" Text="Competitive" OnCheckedChanged="gameTypeCheckedChanged" />
                <div>
                    <asp:CheckBox runat="server" ID="helmetChk" Text="Helmet" Enabled="false" />
                    <br />
                    <asp:CheckBox runat="server" ID="armorChk" Text="Armour" Enabled="false" />
                </div>
            </div>

        </div>
    </div>

    <div>
        $:
        <asp:TextBox runat="server" ID="amountTxt"></asp:TextBox>
        <asp:Button ID="getWeaponsBtn" runat="server" OnClick="GetWeapons_Click" Text="Get Weapons" />
    </div>
    <div>
        Main Weapon:
        <asp:Label runat="server" ID="mainWeaponLbl" Text="None"></asp:Label>
    </div>
    <div>
        Pistol:
        <asp:Label runat="server" ID="pistolLbl" Text="None"></asp:Label>
    </div>
    <div>
        Grenades:
        <asp:Label runat="server" ID="grenadeLbl" Text="None"></asp:Label>
    </div>
    <div>
        Keys:
        <asp:Label runat="server" ID="keysLbl" Text="None"></asp:Label>
    </div>
    <br />

</asp:Content>
