﻿using Microsoft.Maui.Adapters;
using System.Reflection;
using System.Windows.Input;

namespace Microsoft.Maui.Controls;

public partial class VirtualListView : View, IVirtualListView, IVirtualListViewSelector, IVisualTreeElement
{
	static VirtualListView()
	{

	}

	public static readonly BindableProperty PositionInfoProperty = BindableProperty.CreateAttached(
		nameof(PositionInfo),
		typeof(PositionInfo),
		typeof(View),
		default);

	public IVirtualListViewAdapter Adapter
	{
		get => (IVirtualListViewAdapter)GetValue(AdapterProperty);
		set => SetValue(AdapterProperty, value);
	}

	public static readonly BindableProperty AdapterProperty =
		BindableProperty.Create(nameof(Adapter), typeof(IVirtualListViewAdapter), typeof(VirtualListView), default);


	public IView GlobalHeader
	{
		get => (IView)GetValue(GlobalHeaderProperty);
		set => SetValue(GlobalHeaderProperty, value);
	}

	public static readonly BindableProperty GlobalHeaderProperty =
		BindableProperty.Create(nameof(GlobalHeader), typeof(IView), typeof(VirtualListView), default);

	public IView GlobalFooter
	{
		get => (IView)GetValue(GlobalFooterProperty);
		set => SetValue(GlobalFooterProperty, value);
	}

	public static readonly BindableProperty GlobalFooterProperty =
		BindableProperty.Create(nameof(GlobalFooter), typeof(IView), typeof(VirtualListView), default);



	public DataTemplate ItemTemplate
	{
		get => (DataTemplate)GetValue(ItemTemplateProperty);
		set => SetValue(ItemTemplateProperty, value);
	}

	public static readonly BindableProperty ItemTemplateProperty =
		BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(VirtualListView), default);

	public VirtualListViewItemTemplateSelector ItemTemplateSelector
	{
		get => (VirtualListViewItemTemplateSelector)GetValue(ItemTemplateSelectorProperty);
		set => SetValue(ItemTemplateSelectorProperty, value);
	}

	public static readonly BindableProperty ItemTemplateSelectorProperty =
		BindableProperty.Create(nameof(ItemTemplateSelector), typeof(VirtualListViewItemTemplateSelector), typeof(VirtualListView), default);



	public DataTemplate SectionHeaderTemplate
	{
		get => (DataTemplate)GetValue(SectionHeaderTemplateProperty);
		set => SetValue(SectionHeaderTemplateProperty, value);
	}

	public static readonly BindableProperty SectionHeaderTemplateProperty =
		BindableProperty.Create(nameof(SectionHeaderTemplate), typeof(DataTemplate), typeof(VirtualListView), default);

	public VirtualListViewSectionTemplateSelector SectionHeaderTemplateSelector
	{
		get => (VirtualListViewSectionTemplateSelector)GetValue(SectionHeaderTemplateSelectorProperty);
		set => SetValue(SectionHeaderTemplateSelectorProperty, value);
	}

	public static readonly BindableProperty SectionHeaderTemplateSelectorProperty =
		BindableProperty.Create(nameof(SectionHeaderTemplateSelector), typeof(VirtualListViewSectionTemplateSelector), typeof(VirtualListView), default);



	public DataTemplate SectionFooterTemplate
	{
		get => (DataTemplate)GetValue(SectionFooterTemplateProperty);
		set => SetValue(SectionFooterTemplateProperty, value);
	}

	public static readonly BindableProperty SectionFooterTemplateProperty =
		BindableProperty.Create(nameof(SectionFooterTemplate), typeof(DataTemplate), typeof(VirtualListView), default);

	public VirtualListViewSectionTemplateSelector SectionFooterTemplateSelector
	{
		get => (VirtualListViewSectionTemplateSelector)GetValue(SectionFooterTemplateSelectorProperty);
		set => SetValue(SectionFooterTemplateSelectorProperty, value);
	}

	public static readonly BindableProperty SectionFooterTemplateSelectorProperty =
		BindableProperty.Create(nameof(SectionFooterTemplateSelector), typeof(VirtualListViewSectionTemplateSelector), typeof(VirtualListView), default);


	public Maui.SelectionMode SelectionMode
	{
		get => (Maui.SelectionMode)GetValue(SelectionModeProperty);
		set => SetValue(SelectionModeProperty, value);
	}

	public static readonly BindableProperty SelectionModeProperty =
		BindableProperty.Create(nameof(SelectionMode), typeof(Maui.SelectionMode), typeof(VirtualListView), Maui.SelectionMode.None);

	public event EventHandler<SelectedItemsChangedEventArgs> SelectedItemsChanged;

	public event EventHandler<EventArgs> OnRefresh;

	void IVirtualListView.Refresh()
	{
		if (RefreshCommand != null && RefreshCommand.CanExecute(null))
		{
			RefreshCommand.Execute(null);
		}

		OnRefresh?.Invoke(this, EventArgs.Empty);
	}

	public ICommand RefreshCommand
	{
		get => (ICommand)GetValue(RefreshCommandProperty);
		set => SetValue(RefreshCommandProperty, value);
	}

	public static readonly BindableProperty RefreshCommandProperty =
		BindableProperty.Create(nameof(RefreshCommandProperty), typeof(ICommand), typeof(VirtualListView), default);

	public Color RefreshAccentColor
	{
		get => (Color)GetValue(RefreshAccentColorProperty);
		set => SetValue(RefreshAccentColorProperty, value);
	}

	public static readonly BindableProperty RefreshAccentColorProperty =
		BindableProperty.Create(nameof(RefreshAccentColor), typeof(Color), typeof(VirtualListView), null);


	public ListOrientation Orientation
	{
		get => (ListOrientation)GetValue(OrientationProperty);
		set => SetValue(OrientationProperty, value);
	}

	public static readonly BindableProperty OrientationProperty =
		BindableProperty.Create(nameof(Orientation), typeof(ListOrientation), typeof(VirtualListView), ListOrientation.Vertical);


	public View EmptyView
	{
		get => (View)GetValue(EmptyViewProperty);
		set => SetValue(EmptyViewProperty, value);
	}

	public static readonly BindableProperty EmptyViewProperty =
		BindableProperty.Create(nameof(EmptyView), typeof(View), typeof(VirtualListView), null);

	IView IVirtualListView.EmptyView => EmptyView;

	public IVirtualListViewSelector ViewSelector => this;

	public IView Header => GlobalHeader;
	public IView Footer => GlobalFooter;

	

	public event EventHandler DataInvalidated;


	public event EventHandler<ScrolledEventArgs> OnScrolled;

	void IVirtualListView.Scrolled(ScrolledEventArgs args)
	{
		if (ScrolledCommand != null && ScrolledCommand.CanExecute(args))
		{
			ScrolledCommand.Execute(args);
		}

		OnScrolled?.Invoke(this, args);
	}

	public ICommand ScrolledCommand
	{
		get => (ICommand)GetValue(ScrolledCommandProperty);
		set => SetValue(ScrolledCommandProperty, value);
	}

	public IReadOnlyList<ItemPosition> SelectedItems => throw new NotImplementedException();

	public static readonly BindableProperty ScrolledCommandProperty =
		BindableProperty.Create(nameof(ScrolledCommandProperty), typeof(ICommand), typeof(VirtualListView), default);

	public void InvalidateData()
	{
		(Handler as VirtualListViewHandler)?.InvalidateData();

		DataInvalidated?.Invoke(this, new EventArgs());
	}

	public bool SectionHasHeader(int sectionIndex)
		=> SectionHeaderTemplateSelector != null || SectionHeaderTemplate != null;

	public bool SectionHasFooter(int sectionIndex)
		=> SectionFooterTemplateSelector != null || SectionFooterTemplate != null;

	public IView CreateView(PositionInfo position, object data)
		=> position.Kind switch {
			PositionKind.Item => 
				ItemTemplateSelector?.SelectTemplate(data, position.SectionIndex, position.ItemIndex)?.CreateContent() as View
					?? ItemTemplate?.CreateContent() as View,
			PositionKind.SectionHeader =>
				SectionHeaderTemplateSelector?.SelectTemplate(data, position.SectionIndex)?.CreateContent() as View
					?? SectionHeaderTemplate?.CreateContent() as View,
			PositionKind.SectionFooter =>
				SectionFooterTemplateSelector?.SelectTemplate(data, position.SectionIndex)?.CreateContent() as View
					?? SectionFooterTemplate?.CreateContent() as View,
			PositionKind.Header =>
				GlobalHeader,
			PositionKind.Footer =>
				GlobalFooter,
			_ => default
		};

	public void RecycleView(PositionInfo position, object data, IView view)
	{
		if (view is View controlsView)
		{
			controlsView.SetValue(View.BindingContextProperty, data);
		}
	}

	public string GetReuseId(PositionInfo position, object data)
		=> position.Kind switch
		{
			PositionKind.Item =>
				"ITEM_" + (GetDataTemplateId(
					ItemTemplateSelector?.SelectTemplate(data, position.SectionIndex, position.ItemIndex)
					?? ItemTemplate) ?? "0"),
			PositionKind.SectionHeader =>
				"SECTION_HEADER_" + (GetDataTemplateId(
					SectionHeaderTemplateSelector?.SelectTemplate(data, position.SectionIndex)
					?? SectionHeaderTemplate) ?? "0"),
			PositionKind.SectionFooter =>
				"SECTION_FOOTER_" + (GetDataTemplateId(
					SectionFooterTemplateSelector?.SelectTemplate(data, position.SectionIndex)
					?? SectionFooterTemplate) ?? "0"),
			PositionKind.Header =>
				"GLOBAL_HEADER_" + (Header?.GetType()?.FullName ?? "NIL"),
			PositionKind.Footer =>
				"GLOBAL_FOOTER_" + (Footer?.GetType()?.FullName ?? "NIL"),
			_ => "UNKNOWN"
		};

	static PropertyInfo DataTemplateIdPropertyInfo;

	string? GetDataTemplateId(DataTemplate dataTemplate)
	{
		DataTemplateIdPropertyInfo ??= dataTemplate.GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic);

		return DataTemplateIdPropertyInfo.GetValue(dataTemplate)?.ToString();

	}

	public IReadOnlyList<IVisualTreeElement> GetVisualChildren()
	{
		var results = new List<IVisualTreeElement>();

		foreach (var c in logicalChildren)
		{
			if (c.view is IVisualTreeElement vte)
				results.Add(vte);
		}

		return results;
	}

	readonly object lockLogicalChildren = new();
	readonly List<(int section, int item, Element view)> logicalChildren = new();

	public void ViewDetached(PositionInfo position, IView view)
	{
		var oldLogicalIndex = -1;
		lock (lockLogicalChildren)
		{
			Element elem= null;

			for (var i = 0; i < logicalChildren.Count; i++)
			{
				var child = logicalChildren[i];

				if (child.section == position.SectionIndex
					&& child.item == position.ItemIndex)
				{
					elem = child.view;
					oldLogicalIndex = i;
					break;
				}
			}

			if (oldLogicalIndex >= 0)
			{
				logicalChildren.RemoveAt(oldLogicalIndex);
				if (elem != null)
					VisualDiagnostics.OnChildRemoved(this, elem, oldLogicalIndex);
			}
		}
	}

	public void ViewAttached(PositionInfo position, IView view)
	{
		if (view is Element elem)
		{
			lock (lockLogicalChildren)
				logicalChildren.Add((position.SectionIndex, position.ItemIndex, elem));

			VisualDiagnostics.OnChildAdded(this, elem);
		}
	}

	public bool IsItemSelected(int sectionIndex, int itemIndex)
		=> (Handler as VirtualListViewHandler).IsItemSelected(sectionIndex, itemIndex);

	public void OnSelectedItemsChanged(SelectedItemsChangedEventArgs args)
		=> this.SelectedItemsChanged?.Invoke(this, args);

	public void SelectItems(params ItemPosition[] paths)
		=> (Handler as VirtualListViewHandler).SelectItems(paths);

	public void DeselectItems(params ItemPosition[] paths)
		=> (Handler as VirtualListViewHandler).DeselectItems(paths);

	public void ClearSelection()
		=> (Handler as VirtualListViewHandler).ClearSelection();
}
