//using Blazorise;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.CompilerServices;
//using Microsoft.AspNetCore.Components.Rendering;
//using Orion.Framework.Domains;
//using Orion.Framework.Domains.Attributes;
//using Orion.Framework.Helpers;
//using Orion.Framework.Ui.Blazor.Enums;
//using Orion.Framework.Validations;
//using Syncfusion.Blazor.Buttons;
//using Syncfusion.Blazor.Grids;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//namespace Orion.Framework.Ui.Blazor.Components.OXSyncFusion
//{
//    public class OXGridTest<TModel> : OXGridBase<TModel> where TModel : class, IEntity<TModel, int>
//    {
//        public static int seq = 0;
//        protected override void BuildRenderTree(RenderTreeBuilder builder)
//        {
//            // builder.AddMarkupContent(0, "<h1>Hello, world!</h1>\r\n\r\nWelcome to your new app.\r\n\r\n");
//            // builder.AddAttribute(1, "Title", "How is Blazor working for you?");
//            var _columns = GetColumns();

//            //builder.OpenComponent<Row>(seq++);
//            //builder.AddAttribute(seq++, "Margin", Margin.Is1.OnAll);
//            var build = new BuildHelper<TModel>(builder);
//            build.ToRender(
//                build.Row.AddRow().WithMargin(Margin.Is1.OnAll)
//                .AddFields()
//                .AddField().AddFieldBody(button =>
//                {
//                    button.AddContent(0, build.Button.AddSfButton("Inserir", true).IconMaterial("mdi-database-plus").GetContent());
//                    button.AddContent(1, build.Button.AddSfButton("Alterar").IconMaterial("mdi-database-edit").Style(OxStyleButton.Info).GetContent());
//                    button.AddContent(2, build.Button.AddSfButton("Inserir", true).IconMaterial("mdi-database-minus").Style(OxStyleButton.Warning).GetContent());

//                })
//                .AddFields()
//                .AddField(render=>
//                {
//                    render.AddMarkupContent(3, "<h1>Hello, world!</h1>\r\n\r\nWelcome to your new app.\r\n\r\n");
//                }
//                )
//                .GetContent());

//            //builder.AddRow(seq,Margin.Is1.OnAll,(divFields) =>
//            //{
//            //     divFields.AddFields(seq++, (divField) =>
//            //     {
//            //         divField.AddField(seq++, (renderButton) =>
//            //         {
//            //                 renderButton.AddSfButton(seq++, "Inserir", "mdi mdi-database-plus mdi-18px", isPrimary: true);
//            //                 renderButton.AddSfButton(seq++, "Alterar", "mdi mdi-database-edit mdi-18px", "e-btn e-info");
//            //                 renderButton.AddSfButton(seq++, "Excluir", "mdi mdi-database-minus mdi-18px", "e-btn e-warning");
//            //                 if (AllowButtonReport)
//            //                 {
//            //                     renderButton.AddSfButton(seq++, "Relatório", "mdi mdi-cloud-print-outline mdi-18px");
//            //                 }
//            //         },true);                    
//            //     });
            
//                //divFields.AddFields(seq++, (sfGrid)=>
//                //{
//                //    sfGrid.AddMarkupContent(seq++, "\r\n");
//                //    sfGrid.OpenComponent<SfGrid<TModel>>(seq++);
//                //    sfGrid.AddAttribute(seq++, "Toolbar", Tool);
//                //    sfGrid.AddAttribute(seq++, "TValue", typeof(TModel));
//                //    sfGrid.AddAttribute(seq++, "DataSource", DataSource);
//                //    sfGrid.AddAttribute(seq++, "AllowSelection", true);
//                //    sfGrid.AddAttribute(seq++, "AllowFiltering", AllowFilter);
//                //    sfGrid.AddAttribute(seq++, "AllowPaging", AllowPaging);
//                //    sfGrid.AddAttribute(seq++, "ChildContent", (RenderFragment)((gridComponent) =>
//                //    {
//                //        gridComponent.AddMarkupContent(seq++, "\r\n");
//                //        gridComponent.OpenComponent<GridEvents<TModel>>(seq++);
//                //        gridComponent.AddAttribute(seq++, "CommandClicked", RuntimeHelpers.TypeCheck(EventCallback.Factory.Create<CommandClickEventArgs<TModel>>(this, OnCommandClicked)));
//                //        gridComponent.AddAttribute(seq++, "RowSelected", RuntimeHelpers.TypeCheck(EventCallback.Factory.Create<RowSelectEventArgs<TModel>>(this, OnRowSelected)));
//                //        gridComponent.CloseComponent();

//                //        gridComponent.AddMarkupContent(seq++, "\r\n");
//                //        gridComponent.OpenComponent<GridSelectionSettings>(seq++);
//                //        gridComponent.AddAttribute(seq++, "Type", SelectionType.Single);
//                //        gridComponent.CloseComponent();

//                //        gridComponent.AddMarkupContent(seq++, "\r\n");
//                //        gridComponent.OpenComponent<GridPageSettings>(seq++);
//                //        gridComponent.AddAttribute(seq++, "PageSizes", AllowPaging);
//                //        gridComponent.AddAttribute(seq++, "PageCount", 5);
//                //        gridComponent.AddAttribute(seq++, "PageSize", 10);
//                //        gridComponent.CloseComponent();

//                //        //gridComponent.AddMarkupContent(seq++, "\r\n");
//                //        //gridComponent.OpenComponent<GridColumns>(seq++);
//                //        //var seqContent = seq++;
//                //        //var seqGrid = seq++;
//                //        //var seqField = seq++;
//                //        //var seqHeader = seq++;
//                //        //gridComponent.AddAttribute(seqContent, "ChildContent", (RenderFragment)((gridColumn) =>
//                //        //{
                            
//                //        //    //foreach (var column in _columns)
//                //        //    //{
                             
//                //        //        gridColumn.OpenComponent<GridColumn>(seqGrid);

//                //        //        gridColumn.AddAttribute(seqField, "Field", RuntimeHelpers.TypeCheck <string >( "Nome"));
//                //        //        gridColumn.AddAttribute(seqHeader, "HeaderText", RuntimeHelpers.TypeCheck<string>("Nome"));
//                //        //        //gridColumn.AddAttribute(seq+5, "Width", column.Width);

//                //        //        //gridColumn.AddAttribute(seq+6, "AllowEditing", column.AllowEdit);
//                //        //        //gridColumn.AddAttribute(seq+7, "Type", column.Type);

//                //        //        //if (column.IsPrimaryKey) gridColumn.AddAttribute(seq+8, "IsPrimaryKey", column.IsPrimaryKey);
//                //        //        //if (column.IsCheckBox) gridColumn.AddAttribute(seq+9, "DisplayAsCheckBox", column.IsCheckBox);
//                //        //        //if (!column.Format.IsEmpty()) gridColumn.AddAttribute(seq+10, "Format", column.Format);
//                //        //        gridColumn.CloseComponent();                              
//                //        //     //}

//                //        //}));
//                //        //gridComponent.CloseComponent();

//                //    }));                   
//                //    sfGrid.CloseComponent();
//                //});
//            //});
            
//        }
//        private IList<ModelField> GetColumns()
//        {
//            var lstProperties = typeof(TModel).GetProperties();
//            var lstFields = new List<PropertyInfo>();
//            var lstColumns = new List<ModelField>();

//            if (!ColumnFields.IsNullOrEmpty())
//            {
//                lstProperties = ColumnFields.Split(',')
//                    .Where(s => lstProperties.Any(x => x.Name.Equals(s)))
//                    .Select(x => lstProperties.SingleOrDefault(f => f.Name.Equals(x))).ToArray();
                     
//            }
//            var _order = 100;
//            foreach (var prop in lstProperties)
//            {
//                var pType = prop.PropertyType;
//                var pName = prop.Name;

//                var _field = new ModelField
//                {
//                    Field = prop.Name,
//                    Width = "150",
//                    IsAutoFit = false,
//                    Caption = prop.Name,
//                    IsPrimaryKey = false,
//                    AllowEdit = true,
//                    Format = "",
//                    Order = _order++
//                };
//                if (pName.Equals("Id"))
//                {
//                    _field.AllowEdit = false;
//                    _field.Order = 1;
//                    _field.IsPrimaryKey = true;
//                    _field.Width = "60";                 
//                }
//                if (prop.Name.Equals("Nome"))
//                {
//                    //_field.Width = "250";
//                    _field.IsAutoFit = true;
//                    _field.Order = 2;
//                }
//                if (prop.PropertyType.IsCollection())
//                {
//                    _field.Ignore = true;
//                }
//                var attr = prop.GetAttribute<ModelFieldAttribute>();
//                if (attr != null)
//                {
//                    if (attr.Order > 0) _field.Order = attr.Order;

//                    _field.Ignore = attr.Ignore;
//                    _field.IsAutoFit = attr.AutoFit;
//                    _field.AllowEdit = attr.AllowEdit;
//                    _field.Caption = attr.Display.IsEmpty() ? prop.Name : attr.Display;
//                    _field.Field = attr.FieldMapping.IsEmpty() ? prop.Name : attr.FieldMapping;
//                }
//                if (pType == typeof(bool))
//                {
//                    _field.Width = "60";
//                    _field.Type = ColumnType.Boolean;
//                    _field.IsCheckBox = true;
//                }
//                else if (pType == typeof(decimal?) || pType == typeof(decimal))
//                {
//                    _field.Width = "120";
//                    _field.Format = "n2";
//                    _field.Type = ColumnType.Number;
//                }
//                else if (pType == typeof(int?) || pType == typeof(int))
//                {
//                    _field.Width = "60";
//                    _field.Type = ColumnType.Number;
//                }
//                else if (pType == typeof(DateTime?) || pType == typeof(DateTime))
//                {
//                    _field.Width = "130";
//                    _field.Format = "dd/MM/yyyy";
//                    _field.Type = ColumnType.DateTime;
//                }
//                lstColumns.Add(_field);
//            }
//            return lstColumns.Where(f => !f.Ignore).OrderBy(f=>f.Order).ToList();
//        }

//    }
//    internal class ModelField
//    {
//        public int Order { get; set; }
//        public string Field { get; set; }
//        public string Name { get; set; }
//        public string Caption { get; set; }
//        public string Width { get; set; } = "150";
//        public bool IsPrimaryKey { get; set; } = false;
//        public bool AllowSort { get; set; } = true;
//        public bool AllowEdit { get; set; } = true;
//        public bool AllowFilter { get; set; } = true;
//        public bool Ignore { get; set; } = false;
//        public bool IsLookup { get; set; }
//        public bool IsAutoFit { get; set; } = false;
//        public bool IsCheckBox { get; set; } = false;
//        public ColumnType Type { get; set; }
//        public string Format { get; set; }

//    }
    
//    internal class BuildHelper<TModel>
//    {
//        RenderTreeBuilder _builder;
//        protected static int _sequence = 0;
//        public BuildHelper(RenderTreeBuilder _render)
//        {
//            _builder = _render;
//        }
//        public BuildRow Row => new BuildRow();
//        public BuildButton Button => new BuildButton();
//        public BuildGrid<TModel> Grid => new BuildGrid<TModel>();
//        public virtual RenderTreeBuilder ToRender(RenderFragment renderFragment)
//        {
//            _builder.OpenElement(_sequence++, "div");
//            _builder.AddContent(_sequence++, renderFragment);
//            _builder.CloseElement();
//            return _builder;
//        }
//    }
//    internal class BuildGrid<TModel>
//    {
//        IEnumerable<TModel> _dataSource;
//        public BuildGrid<TModel> AddSfGrid(IEnumerable<TModel> datasource)
//        {
//            _dataSource = datasource;
//            return this;
//        }
//        public RenderFragment GetContent(RenderFragment content = null)
//        {
//            return render =>
//            {
//                render.AddMarkupContent(0, "\r\n");
//                render.OpenComponent<SfGrid<TModel>>(1);
//                render.AddAttribute(2, "Data Source", _dataSource);
//                render.CloseComponent();
//                render.AddMarkupContent(0, "\r\n");
//            };
//        }

//    }
//    internal class BuildButton 
//    {
//        private string _content;
//        private bool _isPrimary;
//        private string _cssClass;
//        private string _iconCss;
//        public BuildButton() { }
        
//        public BuildButton AddSfButton(string content,bool isPrimary = false)
//        {
//            _content = content;
//            _isPrimary = isPrimary;
//            return this;
//        }
//        public BuildButton CssClass(string cssClass)
//        {

//            _cssClass = cssClass;
//            return this;
//        }
//        public BuildButton IconCss(string iconCss)
//        {
//            _iconCss = iconCss;
//            return this;
//        }
//        public BuildButton Style(OxStyleButton styleButton)
//        {
//            var _style = HelperEnum.GetDescription(typeof(OxStyleButton), styleButton);
//            _cssClass = $"e-btn {_style}";
//            return this;
//        }
//        public BuildButton IconMaterial(string iconCss)
//        {
//             iconCss = $"mdi {iconCss} mdi-18px";
//            _iconCss = iconCss;
//            return this;
//        }
//        public RenderFragment GetContent(RenderFragment content = null)
//        {
//            return render =>
//            {
//                render.AddMarkupContent(0, "\r\n");
//                render.OpenComponent<SfButton>(1);
//                render.AddAttribute(2, "IsPrimary", _isPrimary);
//                render.AddAttribute(3, "IconCss", _iconCss);
//                render.AddAttribute(4, "Content", _content);
//                if (_cssClass != null) render.AddAttribute(5, "CssClass", _cssClass);
//                render.CloseComponent();
//                render.AddMarkupContent(0, "\r\n");
//            };
//        }
//    }

//    internal class BuildRow 
//    {
//        internal class RowFields
//        {
//            public int Id { get; set; }
//            public RenderFragment Content { get; set; }
//            public List<RowField> Fields { get; set; } = new List<RowField>();

//        }
//        internal class RowField
//        {
//            public int Id { get; set; }
//            public RenderFragment Content { get; set; }
//            public RenderFragment FieldBody { get; set; }

//        }
//        IFluentSpacing _fluentSpacing;
//        private List<RowFields> _getFields = new List<RowFields>();
//        private RowFields _rowFields;
//        private RowField _rowField;
//        public BuildRow()  { }
//        public BuildRow AddRow()
//        {
//            return this;
//        }
//        public BuildRow AddRow(IFluentSpacing margin)
//        {
//            _fluentSpacing = margin;
//            return this;
//        }
//        public BuildRow WithMargin(IFluentSpacing margin)
//        {
//            _fluentSpacing = margin;
//            return this;
//        }
//        public BuildRow AddFields(RenderFragment content = null)
//        {
//            _rowFields = new RowFields();
//            _rowFields.Content = content;
//            _getFields.Add(_rowFields);
//            return this;
//        }
//        public BuildRow AddField(RenderFragment content = null)
//        {
//            Ensure.NotNull(_rowFields,"Element fields not found");
//            _rowField = new RowField();
//            _rowField.Content = content;
//            _rowFields.Fields.Add(_rowField);
//            return this;
//        }
//        public BuildRow AddFieldBody(RenderFragment content = null)
//        {
//            Ensure.NotNull(_rowField, "Element field not found");
//            _rowField.FieldBody = content;
//            return this;
//        }
//        public RenderFragment GetContent(RenderFragment _content = null)
//        {
//            return divRow =>
//            {
//                divRow.AddMarkupContent(1, "\r\n");
//                divRow.OpenComponent<Row>(2);
//                if (_fluentSpacing != null) divRow.AddAttribute(3, "Margin", _fluentSpacing);
//                divRow.AddAttribute(4, "ChildContent", (RenderFragment)((divFields) =>
//                {
//                    foreach (var fields in _getFields)
//                    {
//                        divFields.AddMarkupContent(0, "\r\n");
//                        divFields.OpenComponent<Fields>(1);
//                        divFields.AddAttribute(2, "ChildContent", (RenderFragment)((divField) =>
//                        {
//                            foreach(var field in fields.Fields)
//                            {
//                                divField.AddMarkupContent(0, "\r\n");
//                                divField.OpenComponent<Field>(1);
//                                divField.AddAttribute(2, "ChildContent", (RenderFragment)((divFieldBody) =>
//                                {
//                                    divFieldBody.AddMarkupContent(0, "\r\n");
//                                    divFieldBody.OpenComponent<FieldBody>(1);
//                                    divFieldBody.AddAttribute(2, "ChildContent", (RenderFragment)((divField) =>
//                                    {

//                                    }));
//                                }));
//                                divField.CloseComponent();
//                            }
//                        }));
//                       divFields.CloseComponent();
//                    }

//                }));
//                divRow.CloseComponent();
//            };
//        }

//        private RenderFragment GetFieldsContent(RenderFragment content)
//        {
//            RenderFragment divFields;
//            divFields = render =>
//                {
//                    foreach (var field in _getFields)
//                    {
//                        render.AddMarkupContent(0, "\r\n");
//                        render.OpenComponent<Fields>(1);
//                        render.AddAttribute(2, "ChildContent", GetFieldContent(content));
//                        render.CloseComponent();
//                    }
//                };
//            return _rowFields == null ? content : divFields;
//        }

//        private RenderFragment GetFieldContent(RenderFragment content)
//        {
//            RenderFragment divField;
//            divField = render =>
//            {
//                for (int idx = 0; idx < _rowField.Length; idx++)
//                {
//                    content = _rowField[idx] ?? content;
//                    render.AddMarkupContent(0, "\r\n");
//                    render.OpenComponent<Fields>(1);
//                    render.AddAttribute(2, "ChildContent", GetFieldBodyContent(content));
//                    render.CloseComponent();
//                }
//            };
//            return _rowField == null ? content : divField;
//        }
//        private RenderFragment GetFieldBodyContent(RenderFragment content)
//        {
//            RenderFragment divFieldBody;
//            divFieldBody = divField =>
//            {
//                for (int idx = 0; idx < _rowFieldBody.Length; idx++)
//                {
//                    content = _rowFieldBody[idx] ?? content;
//                    divField.AddMarkupContent(0, "\r\n");
//                    divField.OpenComponent<FieldBody>(1);
//                    divField.AddAttribute(2, "ChildContent", content);
//                    divField.CloseComponent();
//                }
//            };
//            return _rowFieldBody == null ? content : divFieldBody;
//        }

       
//    }

//    public static class RenderTreeExtensions
//    {

       
//        public static RenderTreeBuilder AddField(this RenderTreeBuilder render, int sequence, RenderFragment content,bool withFieldBody=false)
//        {
//            if (withFieldBody)
//            {
//                RenderFragment _body;
//                _body = divFieldBody =>
//                {
//                    divFieldBody.AddMarkupContent(sequence++, "\r\n");
//                    divFieldBody.OpenComponent<FieldBody>(sequence++);
//                    divFieldBody.AddAttribute(sequence++, "ChildContent", content);
//                    divFieldBody.CloseComponent();
//                };
//                content = _body;
//            }
//            render.AddMarkupContent(sequence++, "\r\n");
//            render.OpenComponent<Field>(sequence++);
//            render.AddAttribute(sequence++, "ChildContent", content);
//            render.CloseComponent();
//            return render;
//        }
       
//        public static void AddSfButton(this RenderTreeBuilder render,int sequence, string content, string IconCss = "", string cssClass = "", bool isPrimary = false)
//        {
//            render.AddMarkupContent(sequence++, "\r\n");
//            render.OpenComponent<SfButton>(sequence++);
//            render.AddAttribute(sequence++, "IsPrimary", isPrimary);
//            render.AddAttribute(sequence++, "IconCss", IconCss);
//            render.AddAttribute(sequence++, "Content",content);
//            if (!cssClass.IsEmpty()) render.AddAttribute(sequence++, "CssClass", cssClass);
//            render.CloseComponent();

//        }

//    }
//}
