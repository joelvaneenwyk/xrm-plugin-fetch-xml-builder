﻿using Microsoft.Xrm.Sdk.Metadata;
using Rappen.XRM.Helpers.FetchXML;
using Rappen.XTB.FetchXmlBuilder.Builder;
using Rappen.XTB.FetchXmlBuilder.ControlsClasses;
using Rappen.XTB.FetchXmlBuilder.DockControls;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Rappen.XTB.FetchXmlBuilder.Controls
{
    public partial class orderControl : FetchXmlElementControlBase
    {
        private bool friendly;
        private AttributeMetadata[] attributes;
        private readonly AttributeMetadata[] allattributes;

        public orderControl() : this(null, null, null, null)
        {
        }

        public orderControl(TreeNode node, AttributeMetadata[] attributes, FetchXmlBuilder fetchXmlBuilder, TreeBuilderControl tree)
        {
            InitializeComponent();
            friendly = FetchXmlBuilder.friendlyNames;
            this.attributes = attributes;
            allattributes = fetchXmlBuilder.GetAllAttribues(node.LocalEntityName()).ToArray();
            InitializeFXB(null, fetchXmlBuilder, tree, node);
        }

        protected override void PopulateControls()
        {
            var aggregate = Node.IsFetchAggregate();
            if (!aggregate)
            {
                cmbAttribute.Items.Clear();
                if (attributes != null)
                {
                    foreach (var attribute in attributes)
                    {
                        AttributeItem.AddAttributeToComboBox(cmbAttribute, attribute, false, friendly);
                    }
                }
            }
            else
            {
                cmbAlias.Items.Clear();
                cmbAlias.Items.Add("");
                cmbAlias.Items.AddRange(GetAliases(Tree.tvFetch.Nodes[0]).ToArray());
            }
            cmbAttribute.Enabled = !aggregate;
            cmbAlias.Enabled = aggregate;
        }

        private List<string> GetAliases(TreeNode node)
        {
            var result = new List<string>();
            if (node.Name == "entity" || node.Name == "link-entity")
            {
                foreach (TreeNode child in node.Nodes)
                {
                    if (child.Name == "attribute")
                    {
                        var alias = child.Value("alias");
                        if (!string.IsNullOrEmpty(alias))
                        {
                            result.Add(alias);
                        }
                    }
                }
            }
            foreach (TreeNode child in node.Nodes)
            {
                result.AddRange(GetAliases(child));
            }
            return result;
        }

        protected override ControlValidationResult ValidateControl(Control control)
        {
            if (control == cmbAttribute && cmbAttribute.Enabled)
            {
                if (string.IsNullOrWhiteSpace(cmbAttribute.Text))
                {
                    return new ControlValidationResult(ControlValidationLevel.Error, "Attribute", ControlValidationMessage.IsRequired);
                }
                if (fxb.entities != null)
                {
                    var attributename = cmbAttribute.SelectedItem is AttributeItem item && item.Metadata != null ? item.Metadata.LogicalName : cmbAttribute.Text;
                    if (!allattributes.Any(a => a.LogicalName == attributename))
                    {
                        return new ControlValidationResult(ControlValidationLevel.Warning, "Attribute", ControlValidationMessage.NotInMetadata);
                    }
                    if (!cmbAttribute.Items.OfType<AttributeItem>().Any(a => a.ToString() == cmbAttribute.Text))
                    {
                        return new ControlValidationResult(ControlValidationLevel.Info, "Attribute", ControlValidationMessage.NotShowingNow);
                    }
                }
            }

            if (control == cmbAlias && cmbAlias.Enabled)
            {
                if (string.IsNullOrWhiteSpace(cmbAlias.Text))
                {
                    return new ControlValidationResult(ControlValidationLevel.Error, "Alias", ControlValidationMessage.IsRequired);
                }

                if (!cmbAlias.Items.OfType<string>().Any(i => i == cmbAlias.Text))
                {
                    return new ControlValidationResult(ControlValidationLevel.Warning, "Alias", ControlValidationMessage.InValid);
                }
            }

            return base.ValidateControl(control);
        }

        public override MetadataBase Metadata()
        {
            if (cmbAttribute.SelectedItem is AttributeItem item)
            {
                return item.Metadata;
            }
            return base.Metadata();
        }

        private void cmbAttribute_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            fxb.ShowMetadata(Metadata());
        }

        public override void Focus()
        {
            cmbAttribute.Focus();
        }
    }
}