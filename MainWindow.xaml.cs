using LightingDevice.models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using LightingDevice.models;

namespace LightingDevice
{
    public partial class MainWindow : Window
    {
        private object selectedInstance;
        private MethodInfo selectedMethod;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "DLL files (*.dll)|*.dll|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                LoadAssembly(path);
            }
        }

        private void LoadAssembly(string path)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(path);
                List<Type> types = new List<Type>();

                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IlluminationDevice).IsAssignableFrom(type))
                    {
                        types.Add(type);
                        lbClasses.Items.Add(type.FullName); 
                    }
                }

                lbClasses.SelectionChanged += (sender, e) => LoadMethods(types[lbClasses.SelectedIndex]);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadMethods(Type type)
        {
            try
            {
                selectedInstance = Activator.CreateInstance(type);
                lbMethods.Items.Clear();

                foreach (MethodInfo method in type.GetMethods())
                {
                    lbMethods.Items.Add(method.Name); 
                }

                lbMethods.SelectionChanged += (sender, e) =>
                {
                    selectedMethod = type.GetMethod(lbMethods.SelectedItem.ToString());

                    ParameterInfo[] parameters = selectedMethod.GetParameters();

                    if (parameters.Length > 0)
                    {
                        object[] methodParams = GetMethodParameters(parameters);
                        if (methodParams == null) return; 
                        ExecuteMethod(methodParams);
                    }
                    else
                    {
                        ExecuteMethod(null);
                    }
                };
            } catch(Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
       

        private object[] GetMethodParameters(ParameterInfo[] parameters)
        {
            try
            {
                List<object> methodParams = new List<object>();

                foreach (var param in parameters)
                {
                    string inputValue = Microsoft.VisualBasic.Interaction.InputBox($"Введите значение параметра {param.Name}:", "Ввод параметра", "");

                    if (string.IsNullOrWhiteSpace(inputValue))
                    {
                        MessageBox.Show("Ввод параметра отменен.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return null;
                    }

                    object value = Convert.ChangeType(inputValue, param.ParameterType);
                    methodParams.Add(value);
                }
                return methodParams.ToArray();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }

        private void ExecuteMethod(object[] methodParams)
        {
            try
            {
                if (selectedMethod != null && selectedInstance != null)
                {
                    object result = selectedMethod.Invoke(selectedInstance, methodParams);

                    MessageBox.Show($"Результат выполнения метода: {result}", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Выберите класс и метод для выполнения.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
