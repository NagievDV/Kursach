   M S B A M L   `   `   `  ����L  HGoncharov_Kursach, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Goncharov_KursachGoncharov_Kursach  5      $    Goncharov_Kursach.RequestsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Goncharov_Kursach     5        N� Title$  RequestsPage��5	      .��5
      �  � RowDefinitions 5   
   �� 5      $	��400 �6   �� 5      .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dGridRequests��5         AutoGenerateColumns . 
������  �� ItemStringFormat$  ��5         MinColumnWidth$	 165Q�5      5      �� 5   
   -   5         ��Name$ btnEdit��5      $��50��5      $��25,20,465,20q�5      ���#069e66$ ��Редактировать��5      
�������$��#Poppins)�5      $��20(�5      $��0q�5      $��1��5!      $y�Hidden=�5"      �� 5$   
   -   5        $ btnReturnFromEdit��5$      $��50��5%   	   $��25,20,501,20q�5&   	   ���#069e66$��
Выйти��5(   	   
�������$��#Poppins)�5*   	   $��25(�5+   	   $��0q�5,   	   $��1��5.   	   $y�Hidden=�5/   	   �� 51   
   -   5        $ 	btnDelete��51      $��50��52   	   $��502,20,25,20q�53   	   ���#069e66$��Удалить��55   	   
�������$��#Poppins)�57   	   $��25(�58   	   $��0q�59   	   $��1��5;   	   $y�Hidden=�5<   	   �� 5>   
   -   5        $ btnSave��5>      $��50��5?   	   $��180,20,347,20q�5@   	   ���#069e66$��Сохранить��5B   	   
�������$��#Poppins)�5D   	   $��20(�5E   	   $��0q�5F   	   $��1��5H   	   $y�Hidden=�5I   	   �� 5J   
   -   5        $ btnAdd��5J      $��50��5K   	   $��346,20,180,20q�5L   	   ���#069e66$��Добавить��5N   	   
�������$��#Poppins)�5P   	   $��20(�5Q   	   $��0q�5R   	   $��1��5T   	   $y�Hidden=�5U   	   5V      5W      