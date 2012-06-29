<%@ Page Title="" Language="C#" MasterPageFile="~/UserPage.master" AutoEventWireup="true" CodeBehind="NewMessage.aspx.cs" Inherits="NewMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

    <link href="Scripts/themes/ui-lightness/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.button.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.position.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.autocomplete.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery-ui-1.8.21.custom.js" type="text/javascript"></script>

	<script type="text/javascript">
	    (function ($) {
	        $.widget("ui.combobox", {
	            _create: function () {
	                var input,
					self = this,
					select = this.element.hide(),
					selected = select.children(":selected"),
					value = selected.val() ? selected.text() : "",
					wrapper = this.wrapper = $("<span>")
						.addClass("ui-combobox")
						.insertAfter(select);

	                input = $("<input>")
					.appendTo(wrapper)
					.val(value)
					.addClass("ui-state-default ui-combobox-input")
					.autocomplete({
					    delay: 0,
					    minLength: 0,
					    source: function (request, response) {
					        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
					        response(select.children("option").map(function () {
					            var text = $(this).text();
					            if (this.value && (!request.term || matcher.test(text)))
					                return {
					                    label: text.replace(
											new RegExp(
												"(?![^&;]+;)(?!<[^<>]*)(" +
												$.ui.autocomplete.escapeRegex(request.term) +
												")(?![^<>]*>)(?![^&;]+;)", "gi"
											), "<strong>$1</strong>"),
					                    value: text,
					                    option: this
					                };
					        }));
					    },
					    select: function (event, ui) {
					        ui.item.option.selected = true;
					        self._trigger("selected", event, {
					            item: ui.item.option
					        });
					    },
					    change: function (event, ui) {
					        if (!ui.item) {
					            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i"),
									valid = false;
					            select.children("option").each(function () {
					                if ($(this).text().match(matcher)) {
					                    this.selected = valid = true;
					                    return false;
					                }
					            });
					            if (!valid) {
					                // remove invalid value, as it didn't match anything
					                $(this).val("");
					                select.val("");
					                input.data("autocomplete").term = "";
					                return false;
					            }
					        }
					    }
					})
					.addClass("ui-widget ui-widget-content ui-corner-left");

	                input.data("autocomplete")._renderItem = function (ul, item) {
	                    return $("<li></li>")
						.data("item.autocomplete", item)
						.append("<a>" + item.label + "</a>")
						.appendTo(ul);
	                };

	                $("<a>")
					.attr("tabIndex", -1)
					.attr("title", "Show All Items")
					.appendTo(wrapper)
					.button({
					    icons: {
					        primary: "ui-icon-triangle-1-s"
					    },
					    text: false
					})
					.removeClass("ui-corner-all")
					.addClass("ui-corner-right ui-combobox-toggle")
					.click(function () {
					    // close if already visible
					    if (input.autocomplete("widget").is(":visible")) {
					        input.autocomplete("close");
					        return;
					    }

					    // work around a bug (likely same cause as #5265)
					    $(this).blur();

					    // pass empty string as value to search for, displaying all results
					    input.autocomplete("search", "");
					    input.focus();
					});
	            },

	            destroy: function () {
	                this.wrapper.remove();
	                this.element.show();
	                $.Widget.prototype.destroy.call(this);
	            }
	        });
	    })(jQuery);

	    $(function () {
	        $("#combobox").combobox();
	        $("#toggle").click(function () {
	            $("#combobox").toggle();
	        });
	    });
	</script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">


<div>

    <div class="innerContainer" style="padding:10px;">

        <div class="floatbarLeft" style="padding:10px;">
            <asp:Image ID="imgPhoto" runat="server" Width="100px" Height="100px" />
        </div>
        
        <div class="floatbarLeft" style="padding:10px;">
            <div style="width:100%">
                <asp:Label ID="lblReceiver" runat="server" Text="Receiver">
                </asp:Label>
                <br />



              
            <div class="ui-widget" style="width:80%;">
               <select id="combobox" style="width:100%;">
	                <option value="">Select one...</option>
	                <option value="ActionScript">ActionScript</option>
                </select> <%----%>

                <%--<asp:TextBox ID="tbxSelectReceiver" runat="server"></asp:TextBox>--%>

                

            </div>




            </div>
            <div style="width:98%">
                <asp:Label ID="lblMessageHeader" runat="server" Text="Theme">
                </asp:Label>
                <br />
                <asp:TextBox ID="tbxMessageHeader" MaxLength="100" runat="server" Width="100%"></asp:TextBox>
            </div>
        </div>
        
    </div>

    <div style="padding:10px;">
        <center>
        <asp:TextBox ID="tbxMessageText" MaxLength="4000" TextMode="MultiLine" Width="90%" Height="100px" runat="server"></asp:TextBox>
        </center>
    </div>

    <div style="text-align:center;">
        <asp:ImageButton ID="btnSendMessage" runat="server" 
            ImageUrl="~/App_Themes/MainSkin/img/buttons/snw_button_send.png" />
    </div>

</div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphLeftSideBar" runat="server">
</asp:Content>

