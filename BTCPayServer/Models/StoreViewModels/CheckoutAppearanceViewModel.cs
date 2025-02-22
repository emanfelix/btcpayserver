using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BTCPayServer.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTCPayServer.Models.StoreViewModels
{
    public class CheckoutAppearanceViewModel
    {
        public SelectList PaymentMethods { get; set; }

        public void SetLanguages(LanguageService langService, string defaultLang)
        {
            defaultLang = langService.GetLanguages().Any(language => language.Code == defaultLang) ? defaultLang : "en";
            var choices = langService.GetLanguages().Select(o => new PaymentMethodOptionViewModel.Format() { Name = o.DisplayName, Value = o.Code }).ToArray().OrderBy(o => o.Name);
            var chosen = choices.FirstOrDefault(f => f.Value == defaultLang) ?? choices.FirstOrDefault();
            Languages = new SelectList(choices, nameof(chosen.Value), nameof(chosen.Name), chosen);
            DefaultLang = chosen.Value;
        }
        public SelectList Languages { get; set; }

        [Display(Name = "Default payment method on checkout")]
        public string DefaultPaymentMethod { get; set; }

        [Display(Name = "Requires a refund email")]
        public bool RequiresRefundEmail { get; set; }

        [Display(Name = "Only enable the payment method after user explicitly chooses it")]
        public bool LazyPaymentMethods { get; set; }

        [Display(Name = "Redirect invoice to redirect url automatically after paid")]
        public bool RedirectAutomatically { get; set; }

        [Display(Name = "Auto-detect language on checkout")]
        public bool AutoDetectLanguage { get; set; }

        [Display(Name = "Default language on checkout")]
        public string DefaultLang { get; set; }

        [Display(Name = "Link to a custom CSS stylesheet")]
        public string CustomCSS { get; set; }
        [Display(Name = "Link to a custom logo")]
        public string CustomLogo { get; set; }

        [Display(Name = "Custom HTML title to display on Checkout page")]
        public string HtmlTitle { get; set; }

        public List<PaymentMethodCriteriaViewModel> PaymentMethodCriteria { get; set; }
    }

    public class PaymentMethodCriteriaViewModel
    {
        public string PaymentMethod { get; set; }
        public string Value { get; set; }

        public CriteriaType Type { get; set; }

        public enum CriteriaType
        {
            GreaterThan,
            LessThan
        }
        public static string ToString(CriteriaType type)
        {
            switch (type)
            {
                case CriteriaType.GreaterThan:
                    return "Greater than";
                case CriteriaType.LessThan:
                    return "Less than";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

    }
}
