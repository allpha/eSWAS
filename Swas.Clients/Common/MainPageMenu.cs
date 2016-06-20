namespace Swas.Clients.Common
{

    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    public class MainPageMenu
    {
        public class RootManu
        {
            public string Icon { get; set; }
            public string Title { get; set; }
            public bool IsHeader { get; set; }

            public List<ChildMenu> Items { get; set; }
            public List<string> Permissions { get; set; }
        }

        public class ChildMenu
        {
            public string Title { get; set; }
            public string Url { get; set; }
            public string ControllerName { get; set; }
            public string PermissionName { get; set; }
        }

        private List<RootManu> _menuItemSource { get; set; }

        public MainPageMenu()
        {
            _menuItemSource = new List<RootManu>()
            {
                new RootManu
                {
                    Icon = "icon-home",
                    Title = "მყარი ნარჩენები",
                    IsHeader = false,
                    Items = new List<ChildMenu>()
                    {
                        new ChildMenu
                        {
                            Url = "/SolidWasteAct/",
                            Title = "რეგისტრაცია",
                            ControllerName = "SolidWasteAct",
                            PermissionName = "SolidWasteAct.View"
                        },
                        new ChildMenu
                        {
                            Url = "/SolidWasteActJurnal/",
                            Title = "რეესტრი",
                            ControllerName = "SolidWasteActJurnal",
                            PermissionName = "SolidWasteActJunal.Veiw"
                        },
                        new ChildMenu
                        {
                            Url = "/Agreement/",
                            Title = "ხელშეკრულებები",
                            ControllerName = "Agreement",
                            PermissionName = "Agreement.View"
                        },
                        new ChildMenu
                        {
                            Url = "/Payment/",
                            Title = "გადახდები",
                            ControllerName = "Payment",
                            PermissionName = "Payments.View"
                        },
                    }
                },
                new RootManu
                {
                    Icon = "",
                    Title = "ადმინისტრირება",
                    IsHeader = true,
                    Items = new List<ChildMenu>(),
                    Permissions = new List<string>()
                    {
                        "Region.View",
                        "Landfill.View",
                        "WasteType.View",
                        "Permission.View",
                        "Role.View",
                        "User.View",
                        "User.Customer",
                    }
                },
                new RootManu
                {
                    Icon = "icon-puzzle",
                    Title = "ცნობარი",
                    IsHeader = false,
                    Items = new List<ChildMenu>()
                    {
                        new ChildMenu
                        {
                            Url = "/Region/",
                            Title = "მდებარეობა",
                            ControllerName = "Region",
                            PermissionName = "Region.View"
                        },
                        new ChildMenu
                        {
                            Url = "/Landfill/",
                            Title = "ნაგავსაყრელი",
                            ControllerName = "Landfill",
                            PermissionName = "Landfill.View"

                        },
                        new ChildMenu
                        {
                            Url = "/WasteType/",
                            Title = "ნარჩენის სახეობა",
                            ControllerName = "WasteType",
                            PermissionName = "WasteType.View"

                        },
                        new ChildMenu
                        {
                            Url = "/Reciever/",
                            Title = "მიმღები",
                            ControllerName = "Reciever",
                            PermissionName = "Reciever.View"
                        },
                        new ChildMenu
                        {
                            Url = "/Position/",
                            Title = "მიმღების თანამდებობა",
                            ControllerName = "Position",
                            PermissionName = "Position.View"
                        },
                        new ChildMenu
                        {
                            Url = "/Customer/",
                            Title = "შემომტანი",
                            ControllerName = "Customer",
                            PermissionName = "Customer.View"
                        },

                        new ChildMenu
                        {
                            Url = "/Representative/",
                            Title = "წარმომადგენელი",
                            ControllerName = "Representative",
                            PermissionName = "Representative.View"
                        },
                        new ChildMenu
                        {
                            Url = "/Transporter/",
                            Title = "ტრანსპორტი",
                            ControllerName = "Transporter",
                            PermissionName = "Transporter.View"
                        },
                    }
                },
                new RootManu
                {
                    Icon = "icon-user",
                    Title = "მომხმარებლები",
                    IsHeader = false,
                    Items = new List<ChildMenu>()
                    {
                        new ChildMenu
                        {
                            Url = "/Permission/",
                            Title = "უფლებები",
                            ControllerName = "Permission",
                            PermissionName = "Permission.View"
                        },
                        new ChildMenu
                        {
                            Url = "/Role/",
                            Title = "როლები",
                            ControllerName = "Role",
                            PermissionName = "Role.View"
                        },
                        new ChildMenu
                        {
                            Url = "/User/",
                            Title = "რეგისტრაცია",
                            ControllerName = "User",
                            PermissionName = "User.View"
                        },
                    }
                },
            };
        }

        public string GenerateMenu(string controllerName)
        {
            if (Globals.SessionContext.Current.User == null || Globals.SessionContext.Current.User.Permissions == null) return string.Empty;

            var result = new StringBuilder();

            foreach (var root in _menuItemSource)
            {
                var childMenu = new StringBuilder();

                if (!root.IsHeader && root.Items.Count > 0)
                {
                    childMenu.AppendLine("<ul class=\"sub-menu\">");

                    var isSelected = false;
                    var hasPermission = false;

                    foreach (var child in root.Items)
                        if (Globals.SessionContext.HasPermission(child.PermissionName))
                        {
                            if (controllerName == child.ControllerName)
                            {
                                childMenu.AppendLine("<li class=\"nav-item  active open\">");
                                isSelected = true;
                            }
                            else
                                childMenu.AppendLine("<li class=\"nav-item \">");

                            childMenu.AppendLine(string.Format("<a href=\"/{0}/\" class=\"nav-link \">", child.ControllerName));
                            childMenu.AppendLine(string.Format("<span class=\"title\">{0}</span>", child.Title));
                            childMenu.AppendLine("</a>");

                            childMenu.AppendLine("</li>");
                            hasPermission = true;
                        }

                    childMenu.AppendLine("</ul>");

                    if (hasPermission)
                    {
                        result.AppendLine(string.Format("<li class=\"nav-item start {0}\" >", isSelected ? "active open" : string.Empty));

                        result.AppendLine("<a href=\"javascript:;\" class=\"nav-link nav-toggle\">");
                        result.AppendLine(string.Format("<i class=\"{0}\"></i>", root.Icon));
                        result.AppendLine(string.Format("<span class=\"title\">{0}</span>", root.Title));
                        result.AppendLine("<span class=\"arrow\"></span>");
                        result.AppendLine("</a>");

                        result.AppendLine(childMenu.ToString());
                        result.AppendLine("</li>");
                    }

                }
                else if (root.IsHeader)
                {
                    result.AppendLine("<li class=\"heading\">");
                    result.AppendLine(string.Format("<h3 class=\"uppercase\" style =\"font-family: mtavrulibold\" >{0}</h3>", root.Title));
                    result.AppendLine("</li>");
                }

            }


            return result.ToString();

        }
    }


}

