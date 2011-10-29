/*
Copyright (c) 2011 Petri Tuononen

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be included
in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Web.Mvc;
using System.Text;
using System.Linq.Expressions;
using System.Web.Mvc.Html;

namespace PrjctMngmt.HtmlHelpers
{
    public static class HtmlHelperExtensions 
    {
        public static MvcHtmlString ComboBox(this HtmlHelper html, string name, SelectList items, string selectedValue)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(html.DropDownList(name + "_hidden", items, new { @style = "width: 200px;", @onchange = "$('input#" + name + "').val($(this).val());" }));
            sb.Append(html.TextBox(name, selectedValue, new { @style = "margin-left: -199px; width: 179px; height: 1.2em; border: 0;" }));
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString ComboBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, SelectList items)
        {
            MemberExpression me = (MemberExpression)expression.Body;
            string name = me.Member.Name;

            StringBuilder sb = new StringBuilder();
            sb.Append(html.DropDownList(name + "_hidden", items, new { @style = "width: 200px;", @onchange = "$('input#" + name + "').val($(this).val());" }));
            sb.Append(html.TextBoxFor(expression, new { @style = "margin-left: -199px; width: 179px; height: 1.2em; border: 0;" }));
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}