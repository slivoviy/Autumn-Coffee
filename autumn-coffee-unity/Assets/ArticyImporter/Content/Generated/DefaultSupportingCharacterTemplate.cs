//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Articy.Cozy_Cafe_atricy.Features;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Articy.Cozy_Cafe_atricy
{
    
    
    public class DefaultSupportingCharacterTemplate : Entity, IEntity, IPropertyProvider, IObjectWithFeatureDefaultBasicCharacterFeature
    {
        
        [SerializeField()]
        private ArticyValueDefaultSupportingCharacterTemplateTemplate mTemplate = new ArticyValueDefaultSupportingCharacterTemplateTemplate();
        
        private static Articy.Cozy_Cafe_atricy.Templates.DefaultSupportingCharacterTemplateTemplateConstraint mConstraints = new Articy.Cozy_Cafe_atricy.Templates.DefaultSupportingCharacterTemplateTemplateConstraint();
        
        public Articy.Cozy_Cafe_atricy.Templates.DefaultSupportingCharacterTemplateTemplate Template
        {
            get
            {
                return mTemplate.GetValue();
            }
            set
            {
                mTemplate.SetValue(value);
            }
        }
        
        public static Articy.Cozy_Cafe_atricy.Templates.DefaultSupportingCharacterTemplateTemplateConstraint Constraints
        {
            get
            {
                return mConstraints;
            }
        }
        
        public DefaultBasicCharacterFeatureFeature GetFeatureDefaultBasicCharacterFeature()
        {
            return Template.DefaultBasicCharacterFeature;
        }
        
        protected override void CloneProperties(object aClone, Articy.Unity.ArticyObject aFirstClassParent)
        {
            DefaultSupportingCharacterTemplate newClone = ((DefaultSupportingCharacterTemplate)(aClone));
            if ((Template != null))
            {
                newClone.Template = ((Articy.Cozy_Cafe_atricy.Templates.DefaultSupportingCharacterTemplateTemplate)(Template.CloneObject(newClone, aFirstClassParent)));
            }
            base.CloneProperties(newClone, aFirstClassParent);
        }
        
        public override bool IsLocalizedPropertyOverwritten(string aProperty)
        {
            return base.IsLocalizedPropertyOverwritten(aProperty);
        }
        
        #region property provider interface
        public override void setProp(string aProperty, object aValue)
        {
            if (aProperty.Contains("."))
            {
                Template.setProp(aProperty, aValue);
                return;
            }
            base.setProp(aProperty, aValue);
        }
        
        public override Articy.Unity.Interfaces.ScriptDataProxy getProp(string aProperty)
        {
            if (aProperty.Contains("."))
            {
                return Template.getProp(aProperty);
            }
            return base.getProp(aProperty);
        }
        #endregion
    }
}
