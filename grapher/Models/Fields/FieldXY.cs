﻿using System;
using System.Windows.Forms;

namespace grapher
{
    public class FieldXY
    {
        #region Constructors

        public FieldXY(TextBox xBox, TextBox yBox, CheckBox lockCheckBox, Form containingForm, double defaultData)
        {
            XField = new Field(xBox, containingForm, defaultData);
            YField = new Field(yBox, containingForm, defaultData);
            YField.FormatString = Constants.ShortenedFormatString;
            LockCheckBox = lockCheckBox;
            LockCheckBox.CheckedChanged += new System.EventHandler(CheckChanged);

            XField.Box.Width = (YField.Box.Left + YField.Box.Width - XField.Box.Left - Constants.DefaultFieldSeparation) / 2;
            YField.Box.Width = XField.Box.Width;

            DefaultWidthX = XField.Box.Width;
            DefaultWidthY = YField.Box.Width;

            YField.Box.Left = XField.Box.Left + XField.Box.Width + Constants.DefaultFieldSeparation;

            CombinedWidth = DefaultWidthX + DefaultWidthY + YField.Box.Left - (XField.Box.Left + DefaultWidthX);
            SetCombined();
        }

        #endregion Constructors

        #region Properties

        public double X
        {
            get => XField.Data;
        }

        public double Y
        {
            get
            {
                if (Combined)
                {
                    return X;
                }
                else
                {
                    return YField.Data;
                }
            }
        }

        public CheckBox LockCheckBox { get; }

        public Field XField { get; }

        public Field YField { get; }

        public int CombinedWidth { get; }

        public int Left {
            get
            {
                return XField.Left;
            }
            set
            {
            }
        }

        public int Width
        {
            get
            {
                return CombinedWidth;
            }
            set
            {
            }
        }

        public int Top
        {
            get
            {
                return XField.Top;
            }
            set
            {
            }
        }

        public int Height
        {
            get
            {
                return XField.Height;
            }
            set
            {
            }
        }

        public bool Visible
        {
            get
            {
                return XField.Box.Visible;
            }
        }

        private bool Combined { get; set; }

        private int DefaultWidthX { get; }

        private int DefaultWidthY { get; }


        #endregion Properties

        #region Methods

        private void CheckChanged(object sender, EventArgs e)
        {
            if (LockCheckBox.CheckState == CheckState.Checked)
            {
                SetCombined();
            }
            else
            {
                SetSeparate();
            }
        }

        public void SetCombined()
        {
            Combined = true;
            YField.SetToUnavailable();
            YField.Box.Hide();
            XField.Box.Width = CombinedWidth;
            XField.FormatString = Constants.DefaultFieldFormatString;
        }

        public void SetSeparate()
        {
            Combined = false;

            XField.Box.Width = DefaultWidthX;
            YField.Box.Width = DefaultWidthY;

            XField.FormatString = Constants.ShortenedFormatString;

            if (XField.State == Field.FieldState.Default)
            {
                YField.SetToDefault();
            }
            else
            {
                YField.SetToEntered(XField.Data);
            }

            if (XField.Box.Visible)
            {
                YField.Box.Show();
            }
        }

        public void Show()
        {
            XField.Box.Show();

            if (!Combined)
            {
                YField.Box.Show();
            }
        }

        public void Hide()
        {
            XField.Box.Hide();
            YField.Box.Hide();
        }

        #endregion Methods
    }
}