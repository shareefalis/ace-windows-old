﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VATRP.App.Interfaces;
using VATRP.App.Services;
using VATRP.Core.Model;

namespace VATRP.App.CustomControls
{
    /// <summary>
    /// Interaction logic for CodecsSettingsCtrl.xaml
    /// </summary>
    public partial class CodecsSettingsCtrl : ISettings
    {
        public CodecsSettingsCtrl()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (App.CurrentAccount == null)
                return;
             AudioCodecsListView.Items.Clear();
            foreach (var item in App.CurrentAccount.AudioCodecsList)
            {
                AudioCodecsListView.Items.Add(item);
            }

            VideoCodecsListView.Items.Clear();
            foreach (var item in App.CurrentAccount.VideoCodecsList)
            {
                VideoCodecsListView.Items.Add(item);
            }
        }

        #region ISettings

        public bool IsChanged()
        {
            if (App.CurrentAccount == null)
                return false;

            return true;
        }

        public bool Save()
        {
            if (App.CurrentAccount == null)
                return false;
            
            return true;
        }

        #endregion

        private void AudioCodecsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        public static T FindAncestorOrSelf<T>(DependencyObject obj)
        where T : DependencyObject
        {
            while (obj != null)
            {
                T objTest = obj as T;

                if (objTest != null)
                    return objTest;

                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var lvItem = FindAncestorOrSelf<ListViewItem>(sender as CheckBox);
            var listView = ItemsControl.ItemsControlFromItemContainer(lvItem) as ListView;
            if (listView != null)
            {
                listView.SelectedItem = null;
                var index = listView.ItemContainerGenerator.IndexFromContainer(lvItem);
                listView.SelectedIndex = index;
            }
        }

        private void VideoCodecsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}