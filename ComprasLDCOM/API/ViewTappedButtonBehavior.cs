using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComprasLDCOM.API
{
    public class ViewTappedButtonBehavior : Behavior<View>
    {
        public static readonly BindableProperty AnimationTypeProperty =
            BindableProperty.Create(nameof(AnimationType), typeof(AnimationType), typeof(ViewTappedButtonBehavior), AnimationType.Scale);

        public AnimationType AnimationType
        {
            get { return (AnimationType)GetValue(AnimationTypeProperty); }
            set { SetValue(AnimationTypeProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ViewTappedButtonBehavior), null, defaultBindingMode: BindingMode.TwoWay);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ViewTappedButtonBehavior), null);

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public View AssociatedObject { get; private set; }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;

            if (bindable is Button myButton)
            {
                myButton.Clicked += View_Tapped;
            }
            else if (bindable is Switch mySwitch)
            {
                mySwitch.Toggled += View_Tapped;
            }
            else
            {
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += View_Tapped;
                bindable.GestureRecognizers.Add(tapGestureRecognizer);
            }

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;

            var exists = bindable.GestureRecognizers.FirstOrDefault() as TapGestureRecognizer;

            if (exists != null)
                exists.Tapped -= View_Tapped;

            base.OnDetachingFrom(bindable);
        }

        bool _isAnimating = false;


        void View_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (_isAnimating)
                    return;

                _isAnimating = true;

                var view = (View)sender;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        if (AnimationType == AnimationType.Scale)
                        {
                            await view.ScaleXTo(1.2, 200);
                            await view.ScaleXTo(1, 200);
                        }
                        else if (AnimationType == AnimationType.Fade)
                        {

                            await view.FadeTo(0.6, 150);
                            await view.FadeTo(1, 150);

                        }
                    }
                    finally
                    {
                        if (Command != null)
                        {
                            if (Command.CanExecute(CommandParameter))
                            {
                                Command.Execute(CommandParameter);
                            }
                        }
                        System.Diagnostics.Debug.WriteLine(CommandParameter);

                        _isAnimating = false;
                    }
                });
            }
            catch { }
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
    public enum AnimationType
    {
        Scale,
        Fade
    }
}
