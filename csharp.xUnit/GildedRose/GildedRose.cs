using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GildedRoseKata;

/// <summary>
/// Ce script met à jour les valeurs de qualité et le nombre de jours avant de vendre chaque Item.
/// Pour chaque Item, on teste plusieurs règles avant de lui assigner ses valeurs finales.
/// Le script est divisé en plusieurs fonctions qui peuvent s'adapter facilement à de nouvelles règles.
/// </summary>
public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> p_items)
    {
        Items = p_items;
    }

    // Fonction qui met à jour la qualité de tous les articles.
    public void UpdateQuality()
    {
        foreach (Item item in Items)
        {
            QualityRules(item);
        }
    }

    // Fonction qui teste les règles spécifiques avant la mise à jour.
    public void QualityRules(Item p_item)
    {
        if (p_item.Name == "Sulfuras, Hand of Ragnaros")
            return;
        int QMult = p_item.Name == "Conjured Mana Cake" ? 2 : 1;

        if (p_item.SellIn <= 0)
            QMult *= 2;

        // Règle pour le brie
        if (p_item.Name == "Aged Brie")
        {
            QMult *= -1;
            ApplyChanges(p_item, 1, QMult);
            return;
        }

        // Règles pour les places de concerts
        if (p_item.Name == "Backstage passes to a TAFKAL80ETC concert") 
        {
            if (p_item.SellIn <= 0)
                p_item.Quality = 0;
            else if (p_item.SellIn <= 5)
                QMult *= -3;
            else if (p_item.SellIn <= 10)
                QMult *= -2;
            else
                QMult *= -1;
        }

        if (p_item.Quality == 0 || p_item.Quality > 50)
            QMult *= 0;

        ApplyChanges(p_item, 1, QMult);
    }

    // Applique les changements en fonction des paramètres calculés par la fonction QualityRules.
    public void ApplyChanges(Item p_item, int p_SMultiplicator, int p_QMultiplicator)
    {

        p_item.SellIn -= 1 * p_SMultiplicator;
        p_item.Quality -= 1 * p_QMultiplicator;

        if (p_item.Quality < 0)
            p_item.Quality = 0;

        if (p_item.Quality > 50)
            p_item.Quality = 50;
    }
}