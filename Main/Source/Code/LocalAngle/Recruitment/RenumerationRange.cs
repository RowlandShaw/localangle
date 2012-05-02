using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using LocalAngle.Net;

namespace LocalAngle.Recruitment
{
    /// <summary>
    /// Represents a Renumeration range (for salary information/etc)
    /// </summary>
    public class RemunerationRange : BindableBase
    {
        #region Public Properties

        private string _currency = CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
        /// <summary>
        /// Gets or sets the currency character.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency
        {
            get
            {
                return _currency;
            }
            set
            {
                OnPropertyChanged("Currency", ref _currency, value);
            }
        }

        private decimal? _lowerBound;
        /// <summary>
        /// Gets or sets the lower bound of the range.
        /// </summary>
        /// <value>
        /// The lower bound.
        /// </value>
        public decimal? LowerBound
        {
            get
            {
                return _lowerBound;
            }
            set
            {
                OnPropertyChanged("LowerBound", ref _lowerBound, value);
            }
        }

        private decimal? _upperBound;
        /// <summary>
        /// Gets or sets the upper bound of the range.
        /// </summary>
        /// <value>
        /// The upper bound.
        /// </value>
        public decimal? UpperBound
        {
            get
            {
                return _upperBound;
            }
            set
            {
                OnPropertyChanged("UpperBound", ref _upperBound, value);
            }
        }

        private Recurrence _period = Recurrence.Year;
        /// <summary>
        /// Gets or sets the period for how frequently the amount is paid.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        public Recurrence Period
        {
            get
            {
                return _period;
            }
            set
            {
                OnPropertyChanged("Period", ref _period, value);
            }
        }

        private Boolean _isApproximate;
        /// <summary>
        /// Gets or sets a value indicating whether this range is approximate.
        /// </summary>
        /// <value>
        /// 	<see langword="true"/> if this instance is approximate; otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// Only affects output if the value is greater than 1,000
        /// </remarks>
        public Boolean IsApproximate
        {
            get
            {
                return _isApproximate;
            }
            set
            {
                OnPropertyChanged("IsApproximate", ref _isApproximate, value);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <remarks>
        /// If the 
        /// </remarks>
        public override string ToString()
        {
            if (LowerBound.HasValue)
            {
                StringBuilder bob = new StringBuilder();

                if (!UpperBound.HasValue)
                {
                    bob.Append("From ");
                }

                bob.Append(FormatCurrency(LowerBound.Value));

                if (UpperBound.HasValue && LowerBound.Value != UpperBound.Value)
                {
                    bob.Append(" - ");

                    bob.Append(FormatCurrency(UpperBound.Value));
                }

                if (Period != Recurrence.Year)
                {
                    bob.Append(" per ");
                    bob.Append(Period.ToString());
                }

                return bob.ToString();
            }
            else
            {
                return "Competitve";
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Formats a financial amount.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private string FormatCurrency(decimal value)
        {
            if (IsApproximate && value > 1000)
            {
                value = decimal.Round(value / 1000, 1);

                if (value == decimal.Round(value, 0))
                {
                    return (Currency.Length == 1 ? Currency : "") + value.ToString("f0", CultureInfo.CurrentCulture) + "K" + (Currency.Length > 1 ? " " + Currency : "");
                }
                else
                {
                    return (Currency.Length == 1 ? Currency : "") + value.ToString("f1", CultureInfo.CurrentCulture) + "K" + (Currency.Length > 1 ? " " + Currency : "");
                }
            }
            else
            {
                return (Currency.Length == 1 ? Currency : "") + value.ToString("f2", CultureInfo.CurrentCulture) + (Currency.Length > 1 ? " " + Currency : "");
            }
        }

        #endregion
    }
}
