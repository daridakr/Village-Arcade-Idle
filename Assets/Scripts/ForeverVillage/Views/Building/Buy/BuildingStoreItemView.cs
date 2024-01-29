using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Village
{
    public class BuildingStoreItemView : StoreItemView<Building>
    {
        [SerializeField] private HorizontalLayoutGroup _statsGroup;
        [SerializeField] private BuildingStatView _statTemplate;

        public override void Render(Building buying, int playerBalance)
        {
            base.Render(buying, playerBalance);

            RenderStats(buying.GetStatesWithIcons());
        }

        private void RenderStats(Dictionary<Sprite, int> statsAndIcons)
        {
            if (statsAndIcons == null || statsAndIcons.Count() == 0)
            {
                return;
            }

            foreach (var stat in statsAndIcons)
            {
                BuildingStatView statView = Instantiate(_statTemplate, _statsGroup.transform);
                statView.Render(stat.Key, stat.Value.ToString());
            }
        }
    }
}